using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PropertyChanged;

namespace BooruApp.Views.Controls
{
    [DoNotNotify]
    public partial class PostView : UserControl
    {
        public PostView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}