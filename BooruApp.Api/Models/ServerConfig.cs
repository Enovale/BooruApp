using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace BooruApp.Api.Models
{
    [Serializable]
    public class ServerConfig : INotifyPropertyChanged
    {
        [JsonInclude]
        public string Name { get; set; }
        
        [JsonPropertyName("Provider")]
        public string ProviderId { get; set; }
        
        [JsonInclude]
        public string ApiUrl { get; set; }
        
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}