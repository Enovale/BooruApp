using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace BooruApp.Backend
{
    [Serializable]
    public class BooruConfig : INotifyPropertyChanged
    {
        [JsonInclude]
        public ObservableCollection<ServerConfig> Servers { get; set; } = new();

        [JsonPropertyName("SelectedServer")]
        public int SelectedServerIndex { get; set; } = 0;

        [JsonIgnore]
        public ServerConfig? SelectedServer => Servers.Count > SelectedServerIndex
            ? Servers[SelectedServerIndex]
            : Servers.FirstOrDefault();

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