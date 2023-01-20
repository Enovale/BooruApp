using System;
using System.IO;
using System.Reflection;
using BooruApp.Backend;

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

        private BooruConfig? _config;
    
        public BooruConfig Config
        {
            get => _config ??= new BooruConfig();
            set => _config = value;
        }

        private void EnsureFolderExists(string path)
        {
            /*var dir = Path.GetDirectoryName(path);
            if (dir is null)
            {
                throw new InvalidDataException("Not a valid file or directory path.");
            }
            Directory.CreateDirectory(dir);
            */
            Directory.CreateDirectory(path);
        }
    }
}