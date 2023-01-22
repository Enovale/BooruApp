using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BooruApp.Api.Models;
using BooruApp.Messages;
using BooruApp.Services;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;

namespace BooruApp.ViewModels
{
    public class ImageListViewModel : ViewModelBase
    {
        public ObservableCollection<Post> Items { get; set; } = new();

        private readonly IServerService _serverService;

        public ImageListViewModel(IServerService serverService)
        {
            _serverService = serverService;
        
            Message.Subscribe<NewSearchMessage>(this, NewSearchReceived);
        }

        private void NewSearchReceived(NewSearchMessage e)
        {
            Task.Run(() => Search(e.Search));
        }

        public async Task Search(string search)
        {
            var result = await _serverService.SearchPosts(search);
        
            if (result is not null)
                Items = new(result);
        }

        public void ImageSelected()
        {
        
        }
    }
}