using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PropertyChanged;

namespace BooruApp.Views
{
    [DoNotNotify]
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}