using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reactive.Concurrency;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using BooruApp.Backend;
using Glitonea.Mvvm;
using ReactiveUI;

namespace BooruApp.ViewModels.Controls
{
    public class PostViewModel : ViewModelBase
    {
        private readonly Post _post;
        private Bitmap? _thumbnail;
    
        public Bitmap? Thumbnail
        {
            get => _thumbnail;
            private set => this.RaiseAndSetIfChanged(ref _thumbnail, value);
        }
    
        public PostViewModel(Post post)
        {
            _post = post;
        }

        public async Task LoadThumbnail()
        {
            using var client = new HttpClient();
            using var response = await client.GetAsync(_post.PreviewUrl);
            await using var stream = await response.Content.ReadAsStreamAsync();
            Thumbnail = new Bitmap(stream);
        }
    }
}