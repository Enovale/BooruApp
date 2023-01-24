using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using BooruApp.Api.Models;
using BooruApp.Infrastructure.Messages;
using BooruApp.Infrastructure.Services;
using BooruApp.Views.Windows;
using Glitonea.Extensions;
using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;

namespace BooruApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public BooruConfig Config
            => _appStorageService.Config;

        public int SelectedServerIndex
        {
            get => _appStorageService.Config.SelectedServerIndex;
            set
            {
                if (value >= 0)
                {
                    _appStorageService.Config.SelectedServerIndex = value;
                    _appStorageService.SaveConfig();
                }
            }
        }

        public string SearchText { get; set; } = string.Empty;

        private readonly IAppStorageService _appStorageService;

        public MainViewModel(IAppStorageService appStorageService)
        {
            _appStorageService = appStorageService;

            Message.Subscribe<SettingsAppliedMessage>(this, SettingsApplied);
        }

        private void SettingsApplied(SettingsAppliedMessage obj)
        {
            OnPropertyChanged(nameof(SelectedServerIndex));
        }

        public void ExitApplication()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime d)
                d.TryShutdown();
        }

        public void OpenSettingsCommand()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime d)
                new SettingsWindow().ShowDialog<SettingsViewModel>(Application.Current.GetMainWindow());
        }

        public void SearchButtonPressed()
        {
            new NewSearchMessage(SearchText).Broadcast();
        }
    }
}