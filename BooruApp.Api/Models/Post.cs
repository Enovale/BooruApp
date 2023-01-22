using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BooruApp.Api.Models
{
    public class Post : INotifyPropertyChanged
    {
        public string FullImageUrl { get; set; }
        public string PreviewImageUrl { get; set; }
    
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}