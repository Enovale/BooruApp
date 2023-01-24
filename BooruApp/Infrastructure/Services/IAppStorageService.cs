using System.ComponentModel;
using System.Threading.Tasks;
using BooruApp.Api.Models;
using Glitonea.Mvvm;

namespace BooruApp.Infrastructure.Services
{
    public interface IAppStorageService : IService, INotifyPropertyChanged
    {
        public string AppName { get; }
    
        public string AppDataDirectory { get; }
        public string PluginDirectory { get; }
        public string PluginDepDirectory { get; }
        public string CacheDirectory { get; }
    
        public string ConfigDirectory { get; }
        public string ConfigFilePath { get; }

        public BooruConfig Config { get; }

        public void SaveConfig();

        public Task SaveConfigAsync();
    }
}