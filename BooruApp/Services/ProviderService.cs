using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using BooruApp.Api;

namespace BooruApp.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IAppStorageService _appStorageService;
        
        private readonly List<IProvider> _providerRegistry = new();
        public IReadOnlyCollection<IProvider> Providers => _providerRegistry.AsReadOnly();

        public ProviderService(IAppStorageService appStorageService)
        {
            _appStorageService = appStorageService;
            GetProviders();
        }

        public IProvider? GetProvider(string idName)
        {
            return Providers.SingleOrDefault(p => p?.IdName == idName, null);
        }

        public void RegisterPlugin(IProvider? provider)
        {
            if (provider is null)
                throw new NullReferenceException("Provided plugin is null.");

            if (!_providerRegistry.Contains(provider))
                _providerRegistry.Add(provider);
        }

        private void GetProviders()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            foreach (var path in Directory.EnumerateFiles(_appStorageService.PluginDirectory, "*.dll"))
            {
                //Server.Logger.Log(LogSeverity.Special,
                //    $"[PluginLoader] Loading plugin: {Path.GetFileNameWithoutExtension(path)}");
                Assembly asm;
                try
                {
                    asm = Assembly.LoadFrom(path);
                }
                catch
                {
                    //Server.Logger.Log(LogSeverity.Error,
                    //    $"[PluginLoader] Couldn't load {path}. Make sure it is a .NET assembly.");
                    continue;
                }

                var anyFound = false;
                foreach (var type in asm.GetTypes().Where(t => typeof(IProvider).IsAssignableFrom(t)))
                {
                    anyFound = true;

                    IProvider? instance;
                    try
                    {
                        instance = (IProvider?)Activator.CreateInstance(type, null);
                    }
                    catch (Exception e)
                    {
                        //Server.Logger.Log(LogSeverity.Error,
                        //    $"[PluginLoader] Could not create instance of {pluginType.Name}: {e}");
                        continue;
                    }

                    try
                    {
                        RegisterPlugin(instance);
                    }
                    catch (Exception e)
                    {
                        //Server.Logger.Log(LogSeverity.Error, $"[PluginLoader] Unable to register {instance!.ID}: {e}");
                        continue;
                    }
                }

                if (!anyFound)
                {
                    //Server.Logger.Log(LogSeverity.Error,
                    //    $"[PluginLoader] Could not find a plugin type in {asm.GetName().Name}, " +
                    //    $"did you implement the Plugin base?");
                    continue;
                }

                //((Server)server).OnAllPluginsLoaded();
                //Server.Logger.Log(LogSeverity.Special, "[PluginLoader] Plugins loaded.");
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object? sender, ResolveEventArgs args)
        {
            // Ignore missing resources
            if (args.Name.Contains(".resources"))
                return null;

            // check for assemblies already loaded
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
                return assembly;

            // Try to load by filename - split out the filename of the full assembly name
            // and append the base path of the original assembly (ie. look in the same dir)
            var filename = args.Name.Split(',')[0] + ".dll".ToLower();

            var asmFile = Path.Combine(_appStorageService.PluginDepDirectory, filename);

            try
            {
                return Assembly.LoadFrom(asmFile);
            }
            catch (Exception e)
            {
                //Server.Logger.Log(LogSeverity.Error, $"[PluginLoader] Error loading dependency, {e}");
                return null;
            }
        }
    }
}