using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BooruApp.Api.Helpers;
using BooruApp.Api.Models;
using BooruApp.Infrastructure;
using BooruApp.Infrastructure.Models;
using BooruApp.Infrastructure.Services;
using Glitonea.Mvvm;

namespace BooruApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public BooruConfig NewConfig { get; set; }

        public IEnumerable<RegisteredServerProvider> AvailableProviders
            => _serverService.Providers.Values;

        public ObservableCollection<ServerConfig> SelectedServers { get; set; } = new();

        public bool Dirty
        {
            get => NewConfig.Dirty;
            set => NewConfig.Dirty = value;
        }

        public string DirtyIndicator => Dirty ? "*" : "";

        private readonly IAppStorageService _appStorageService;
        private readonly IServerService _serverService;

        public SettingsViewModel(IAppStorageService appStorageService, IServerService serverService)
        {
            _appStorageService = appStorageService;
            _serverService = serverService;

            CloseWindowCommand = BasicCommand.Create<Window>(CloseWindow);
            ApplyConfigAndCloseWindowCommand = BasicCommand.Create<Window>(ApplyConfigAndCloseWindow);

            CopyConfig(false);
        }

        private void CopyConfig(bool disposeOld = true)
        {
            if (disposeOld)
                NewConfig.DirtyChanged -= ConfigDirtied;
            
            NewConfig = new(_appStorageService.Config);
            NewConfig.DirtyChanged += ConfigDirtied;
        }

        private void ConfigDirtied(object? sender, bool isdirty)
        {
            OnPropertyChanged(nameof(Dirty));
            OnPropertyChanged(nameof(DirtyIndicator));

            if (isdirty)
            {
                if (Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime s)
                {
                    Task.Run(ApplyConfig);
                    return;
                }
            }
        }

        public ICommand ApplyConfigAndCloseWindowCommand { get; }
        
        public void ApplyConfigAndCloseWindow(Window window)
        {
            ApplyConfig();
            CloseWindow(window);
        }

        public void ApplyConfig()
        {
            _appStorageService.Config.CopyFrom(NewConfig);
            _appStorageService.SaveConfig();
            
            CopyConfig();
        }

        public ICommand CloseWindowCommand { get; }
        
        public void CloseWindow(Window window)
        {
            window.Close(this);
        }

        public void OnProviderChosen(object sender)
        {
        }

        public void AddServer()
        {
            var newItem = new ServerConfig();
            NewConfig.Servers.Add(newItem);
            SelectedServers.Clear();
            SelectedServers.Add(newItem);
        }

        public void RemoveSelectedServers()
        {
            while (SelectedServers.Count > 0)
            {
                NewConfig.Servers.Remove(SelectedServers.First());
            }

            if (NewConfig.Servers.Count > 0)
                SelectedServers.Add(NewConfig.Servers.Last());
        }
    }
}