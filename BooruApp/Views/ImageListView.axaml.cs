using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PropertyChanged;

namespace BooruApp.Views
{
    [DoNotNotify]
    public partial class ImageListView : UserControl
    {
        public ImageListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}