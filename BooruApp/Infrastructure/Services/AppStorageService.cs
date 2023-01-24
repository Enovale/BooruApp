using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using BooruApp.Api.Models;
using PropertyChanged;

namespace BooruApp.Infrastructure.Services
{
    public class AppStorageService : IAppStorageService
    {
        private static string _appName { get; } =
            Assembly.GetExecutingAssembly().GetName().Name!;
    
        private static string _appDataDirectory { get; } =
            Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                _appName.ToLower());
    
        private static string _pluginDirectory { get; } =
            Path.Join(_appDataDirectory, "Plugins");
    
        private static string _pluginDepDirectory { get; } =
            Path.Join(_pluginDirectory, "Dependencies");

        private static string _cacheDirectory { get; } =
            Path.Combine(_appDataDirectory, "Cache");
    
        private static string _configDirectory { get; } =
            Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                _appName.ToLower());

        [DoNotNotify]
        public string AppName => _appName;

        [DoNotNotify]
        public string AppDataDirectory
        {
            get
            {
                EnsureFolderExists(_appDataDirectory);
                return _appDataDirectory;
            }
        }

        [DoNotNotify]
        public string PluginDirectory
        {
            get
            {
                EnsureFolderExists(_pluginDirectory);
                return _pluginDirectory;
            }
        }

        [DoNotNotify]
        public string PluginDepDirectory
        {
            get
            {
                EnsureFolderExists(_pluginDepDirectory);
                return _pluginDepDirectory;
            }
        }
    
        [DoNotNotify]
        public string CacheDirectory
        {
            get
            {
                EnsureFolderExists(_cacheDirectory);
                return _cacheDirectory;
            }
        }
    
        [DoNotNotify]
        public string ConfigDirectory
        {
            get
            {
                EnsureFolderExists(_configDirectory);
                return _configDirectory;
            }
        }

        [DoNotNotify]
        public string ConfigFilePath => Path.Combine(ConfigDirectory, "config.json");
        
        public BooruConfig Config { get; }

        private Task? _configTask;

        public AppStorageService()
        {
            Config = Task.Run(LoadConfig).Result;
            SaveConfig();
        }

        public void SaveConfig()
        {
            if (_configTask is null || _configTask.IsCompleted)
            {
                _configTask = Task.Run(SaveConfigAsync);
            }
        }

        public async Task SaveConfigAsync()
        {
            await using var file = File.Create(ConfigFilePath);
            await JsonSerializer.SerializeAsync(file, Config, new JsonSerializerOptions() { WriteIndented = true});
            Config.Dirty = false;
        }

        public async Task<BooruConfig> LoadConfig()
        {
            if (!File.Exists(ConfigFilePath))
                return new();

            await using var file = File.OpenRead(ConfigFilePath);
            try
            {
                var loadedConfig = await JsonSerializer.DeserializeAsync<BooruConfig>(file) ?? new();
                loadedConfig.Dirty = false;
                return loadedConfig;
            }
            catch (Exception e)
            {
                Trace.TraceError("Configuration deserialization error: {0}", e);
                return new();
            }
        }

        private void EnsureFolderExists(string path)
        {
            Directory.CreateDirectory(path);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}