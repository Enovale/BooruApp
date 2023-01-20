using Avalonia.Controls;
using Avalonia.ReactiveUI;
using BooruApp.ViewModels;

namespace BooruApp.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();
    }
}