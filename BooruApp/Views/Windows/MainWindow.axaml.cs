using System.Threading.Tasks;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using BooruApp.Services;
using BooruApp.ViewModels.Windows;
using Glitonea;
using PropertyChanged;
using ReactiveUI;

namespace BooruApp.Views.Windows
{
    [DoNotNotify]
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.WhenActivated(d => d(ViewModel!.ShowSettingsDialog.RegisterHandler(DoShowSettingsDialogueAsync)));
        }

        private async Task DoShowSettingsDialogueAsync(InteractionContext<SettingsDialogViewModel, SettingsDialogViewModel> interaction)
        {
            var dialog = new SettingsDialog
            {
                DataContext = interaction.Input
            };

            var result = await dialog.ShowDialog<SettingsDialogViewModel?>(this);
            interaction.SetOutput(result);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}