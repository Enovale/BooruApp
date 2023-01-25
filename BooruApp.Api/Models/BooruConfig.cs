using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using BooruApp.Api.Helpers;
using BooruApp.Api.Interfaces;

namespace BooruApp.Api.Models
{
    [Serializable]
    public class BooruConfig : INotifyPropertyChanged, IDirtyObject
    {
        [JsonInclude]
        public ObservableChangesCollection<ServerConfig> Servers { get; set; }
            = new ObservableChangesCollection<ServerConfig>();


        [JsonPropertyName("SelectedServer")]
        public int SelectedServerIndex { get; set; }

        [JsonIgnore]
        public ServerConfig? SelectedServer => Servers.Count > SelectedServerIndex
            ? Servers[SelectedServerIndex]
            : Servers.FirstOrDefault();

        [JsonIgnore]
        public bool Dirty { get; set; }

        [JsonConstructor]
        public BooruConfig()
        {
            Servers.CollectionChanged += ServersChanged;

            Dirty = false;
        }

        public BooruConfig(BooruConfig copy)
        {
            CopyFrom(copy);
            Dirty = false;
        }

        public void CopyFrom(BooruConfig other)
        {
            Servers = new ObservableChangesCollection<ServerConfig>(other.Servers.Select(s => new ServerConfig(s)));
            SelectedServerIndex = other.SelectedServerIndex;

            Servers.CollectionChanged += ServersChanged;
        }

        ~BooruConfig()
        {
            Servers.CollectionChanged -= ServersChanged;
        }

        private void ServersChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Servers));
            SelectedServerIndex = Math.Clamp(SelectedServerIndex, 0, Servers.Count);
        }

        public delegate void DirtyChangedEventHandler(object? sender, bool isDirty);

        public event DirtyChangedEventHandler? DirtyChanged;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            if (propertyName == nameof(Dirty) || propertyName == nameof(SelectedServer))
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return;
            }

            var dirty = Dirty;
            Dirty = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (Dirty != dirty)
                DirtyChanged?.Invoke(this, Dirty);
        }
    }
}