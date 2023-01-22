using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BooruApp.Api.Models
{
    public class Post : INotifyPropertyChanged
    {
        public string? FullImageUrl { get; }
        public string? PreviewImageUrl { get; }

        public Post(string? fullImageUrl, string? previewImageUrl)
            => (FullImageUrl, PreviewImageUrl) = (fullImageUrl, previewImageUrl);
    
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}