using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using BooruApp.Services;
using BooruApp.ViewModels.Controls;
using Glitonea.Extensions;
using Glitonea.Mvvm;
using ReactiveUI;

namespace BooruApp.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<PostViewModel> Images { get; set; } = new();
        public bool NoServersConfigured => _serverService.ServerConfig is null;

        private readonly IServerService _serverService;
        private readonly IAppStorageService _appStorageService;

        public MainWindowViewModel(IServerService serverService, IAppStorageService appStorageService)
        {
            _serverService = serverService;
            _appStorageService = appStorageService;

            _appStorageService.Config.Servers.CollectionChanged += (sender, args) =>
            {
                this.RaisePropertyChanged(nameof(NoServersConfigured));
            };

            ShowSettingsDialog = new();

            OpenSettingsCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var result = await ShowSettingsDialog.Handle(
                    new SettingsDialogViewModel(_serverService, _appStorageService.Config)
                );
            });

            Task.Run(FetchImages);
        }

        private async Task FetchImages()
        {
            var images = await _serverService.SearchPosts(new(), "*");
            Images = new(images.Select(p => new PostViewModel(p)));
            
            foreach (var post in Images)
            {
                await post.LoadThumbnail();
            }
        }

        public void ExitApplication()
        {
            Application.Current
                .GetDesktopLifetime()
                .TryShutdown();
        }

        public ICommand OpenSettingsCommand { get; }
        public Interaction<SettingsDialogViewModel, SettingsDialogViewModel?> ShowSettingsDialog { get; }
    }
}