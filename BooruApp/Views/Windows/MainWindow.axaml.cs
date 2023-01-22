using Avalonia.Markup.Xaml;
using Glitonea;
using PropertyChanged;

namespace BooruApp.Views
{
    [DoNotNotify]
    public partial class MainWindow : WindowEx
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}