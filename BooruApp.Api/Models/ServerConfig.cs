using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using BooruApp.Api.Interfaces;
using PropertyChanged;

namespace BooruApp.Api.Models
{
    [Serializable]
    public class ServerConfig : INotifyPropertyChanged, IDirtyObject
    {
        [JsonInclude]
        public string? Name { get; set; }
        
        [JsonPropertyName("Provider")]
        public string? ProviderId { get; set; }
        
        [JsonInclude]
        public string? ApiUrl { get; set; }

        [JsonIgnore]
        [DoNotNotify]
        public bool Dirty { get; set; } = false;

        public ServerConfig()
        {
        }
        
        public ServerConfig(ServerConfig copy)
            => (Name, ProviderId, ApiUrl) = (copy.Name, copy.ProviderId, copy.ApiUrl);

        public event BooruConfig.DirtyChangedEventHandler? DirtyChanged;
        
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            var dirty = Dirty;
            Dirty = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (Dirty != dirty)
                DirtyChanged?.Invoke(this, Dirty);
        }
    }
}