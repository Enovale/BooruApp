using Avalonia.Controls;
using Avalonia.ReactiveUI;
using BooruApp.ViewModels;

namespace BooruApp.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}