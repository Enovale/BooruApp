using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using BooruApp.Api.Models;
using BooruApp.Infrastructure.Messages;
using BooruApp.Infrastructure.Services;
using BooruApp.Views.Windows;
using Glitonea.Extensions;
using Glitonea.Mvvm;
using PropertyChanged;

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
                _appStorageService.Config.SelectedServerIndex = value;
                _appStorageService.SaveConfig();
            }
        }

        public string SearchText { get; set; } = string.Empty;

        private readonly IAppStorageService _appStorageService;

        public MainViewModel(IAppStorageService appStorageService)
        {
            _appStorageService = appStorageService;
            PropertyChanged += (sender, args) => Console.WriteLine(args.PropertyName + " " + SelectedServerIndex);
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