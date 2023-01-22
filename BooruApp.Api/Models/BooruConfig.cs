using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace BooruApp.Api.Models
{
    [Serializable]
    public class BooruConfig : INotifyPropertyChanged
    {
        [JsonInclude]
        public ObservableCollection<ServerConfig> Servers { get; set; } = new ObservableCollection<ServerConfig>();

        [JsonPropertyName("SelectedServer")]
        public int SelectedServerIndex { get; set; } = 0;

        [JsonIgnore]
        public ServerConfig? SelectedServer => Servers.Count > SelectedServerIndex
            ? Servers[SelectedServerIndex]
            : Servers.FirstOrDefault();

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}