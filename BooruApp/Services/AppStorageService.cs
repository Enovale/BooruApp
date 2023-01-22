using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using BooruApp.Api.Models;

namespace BooruApp.Services
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

        public string AppName => _appName;

        public string AppDataDirectory
        {
            get
            {
                EnsureFolderExists(_appDataDirectory);
                return _appDataDirectory;
            }
        }

        public string PluginDirectory
        {
            get
            {
                EnsureFolderExists(_pluginDirectory);
                return _pluginDirectory;
            }
        }

        public string PluginDepDirectory
        {
            get
            {
                EnsureFolderExists(_pluginDepDirectory);
                return _pluginDepDirectory;
            }
        }
    
        public string CacheDirectory
        {
            get
            {
                EnsureFolderExists(_cacheDirectory);
                return _cacheDirectory;
            }
        }
    
        public string ConfigDirectory
        {
            get
            {
                EnsureFolderExists(_configDirectory);
                return _configDirectory;
            }
        }

        public string ConfigFilePath => Path.Combine(ConfigDirectory, "config.json");
        
        public BooruConfig Config { get; }

        public AppStorageService()
        {
            Config = Task.Run(LoadConfig).Result;
            Task.Run(SaveConfig);
            
            Config.PropertyChanged += ConfigPropertyChanged;
        }

        private void ConfigPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Task.Run(SaveConfig);
        }

        public async Task SaveConfig()
        {
            await using var file = File.OpenWrite(ConfigFilePath);
            await JsonSerializer.SerializeAsync(file, Config, new JsonSerializerOptions() { WriteIndented = true});
        }

        public async Task<BooruConfig> LoadConfig()
        {
            if (!File.Exists(ConfigFilePath))
                return new();

            await using var file = File.OpenRead(ConfigFilePath);
            return await JsonSerializer.DeserializeAsync<BooruConfig>(file) ?? new();
        }

        private void EnsureFolderExists(string path)
        {
            Directory.CreateDirectory(path);
        }
    }
}