using System.Collections.ObjectModel;
using BooruApp.Backend;
using BooruApp.Services;
using Glitonea.Mvvm;

namespace BooruApp.ViewModels.Windows;

public class SettingsDialogViewModel : ViewModelBase
{
    public BooruConfig Config { get; set; }

    private IServerService _serverService;
    
    public SettingsDialogViewModel(IServerService serverService, BooruConfig config)
    {
        _serverService = serverService;
        Config = config;
        
        // TODO set up combobox for provider
        //var fontComboBox = this.Find<ComboBox>("providerComboBox");
        //fontComboBox.Items = providerService.Providers;
        //fontComboBox.SelectedIndex = providerService.Providers.IndexOf(providerService.CurrentProvider);
    }

    public void AddServer()
    {
        Config.Servers.Add(new ServerConfig());
    }
}