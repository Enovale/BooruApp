using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PropertyChanged;

namespace BooruApp.Views.Controls
{
    [DoNotNotify]
    public partial class Post : UserControl
    {
        public Post()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}