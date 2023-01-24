using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Glitonea;
using PropertyChanged;

namespace BooruApp.Views.Windows
{
    [DoNotNotify]
    public partial class SettingsWindow : WindowEx
    {
        public SettingsWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}