using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BooruApp.Api.Models;
using BooruApp.Services;
using Glitonea.Mvvm;

namespace BooruApp.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        public ObservableCollection<ServerConfig> ServerConfigs
            => _appStorageService.Config.Servers;

        internal IEnumerable<RegisteredServerProvider> AvailableProviders
            => _serverService.Providers.Values;

        private readonly IAppStorageService _appStorageService;
        private readonly IServerService _serverService;

        public SettingsViewModel(IAppStorageService appStorageService, IServerService serverService)
        {
            _appStorageService = appStorageService;
            _serverService = serverService;
        }
    }
}