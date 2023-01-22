using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using BooruApp.Messages;
using BooruApp.Views.Windows;
using Glitonea.Extensions;
using Glitonea.Mvvm;

namespace BooruApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public string SearchText { get; set; } = string.Empty;

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