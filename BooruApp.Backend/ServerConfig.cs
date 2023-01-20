using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BooruApp.Backend
{
    [Serializable]
    public class ServerConfig : IServerConfig, INotifyPropertyChanged
    {
        public string? Provider { get; set; }
        
        public string? ApiUrl { get; set; }
        
        public string? Name { get; set; }

        public ServerConfig()
        {
        }
        
        public ServerConfig(string name, string provider, string apiUrl)
            => (Name, Provider, ApiUrl) = (name, provider, apiUrl);
    
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}