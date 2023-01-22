using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BooruApp.Api;
using BooruApp.Api.Models;

namespace BooruApp.Services
{
    public class ServerService : IServerService
    {
        public IReadOnlyDictionary<string, RegisteredServerProvider> Providers => _providerRegistry.AsReadOnly();

        private readonly Dictionary<string, RegisteredServerProvider> _providerRegistry = new();

        private RegisteredServerProvider? _currentProvider
            => _providerRegistry!.GetValueOrDefault(_appStorageService.Config.SelectedServer?.ProviderId);

        private string? _currentApiUrl
            => _appStorageService.Config.SelectedServer?.ApiUrl;

        private readonly IAppStorageService _appStorageService;

        public ServerService(IAppStorageService appStorageService)
        {
            _appStorageService = appStorageService;
            
            InitializeTypeRegistry();
        }

        private void InitializeTypeRegistry()
        {
            foreach (var file in Directory.EnumerateFiles(_appStorageService.PluginDirectory, "*.dll"))
            {
                if (!File.Exists(file))
                {
                    Trace.TraceError("The plugin file '{0}' no longer exists.", file);
                    continue;
                }

                Assembly asm;
                try
                {
                    asm = Assembly.LoadFrom(file);
                }
                catch (Exception e)
                {
                    Trace.TraceError("The plugin file '{0}' failed to be loaded: {1}", file, e);
                    continue;
                }

                IEnumerable<Type> types;
                try
                {
                    types = asm.ExportedTypes;
                }
                catch (Exception e)
                {
                    Trace.TraceError("Failed to enumerate types for plugin file '{0}': {1}", file, e);
                    continue;
                }
                
                foreach (var type in types)
                {
                    if (!type.IsAssignableTo(typeof(ServerProvider)))
                        continue;

                    var attr = type.GetCustomAttribute<ServerProviderAttribute>();

                    if (attr is null)
                    {
                        Trace.TraceError(
                            "A plugin type was found but it is not decorated with '{0}'. Please add it to your class for the class to function.",
                            nameof(ServerProviderAttribute));
                        continue;
                    }

                    ServerProvider? instance;
                    try
                    {
                        instance = (ServerProvider?)Activator.CreateInstance(type);

                        if (instance is null)
                        {
                            Trace.TraceError(
                                "The plugin type '{0}' was instantiated successfully but the returned instance is null. Wtf??",
                                type.Name);
                            continue;
                        }
                    }
                    catch (Exception e)
                    {
                        Trace.TraceError("The plugin type '{0}' failed to be instantiated: {1}", type.Name, e);
                        continue;
                    }

                    _providerRegistry.Add(attr.IdName, new(instance, attr));
                }
            }
        }

        private bool InitServerProvider()
        {
            if (_currentProvider is null || _currentApiUrl is null)
                return false;
            
            _currentProvider.ProviderInstance.InternalInit(_currentApiUrl);
            return true;
        }

        public async Task<bool> CompatibilityCheck()
        {
            if (!InitServerProvider())
                return false;
            
            return await _currentProvider!.ProviderInstance.CompatibilityCheck();
        }

        public async Task<List<Post>?> SearchPosts(params string[] tags)
        {
            if (!InitServerProvider())
                return null;
            
            return await _currentProvider!.ProviderInstance.SearchPosts(tags);
        }
    }
}