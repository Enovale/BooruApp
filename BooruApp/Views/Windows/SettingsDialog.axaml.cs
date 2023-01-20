using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BooruApp.Services;
using DynamicData;
using Glitonea;
using PropertyChanged;

namespace BooruApp.Views.Windows;

[DoNotNotify]
public partial class SettingsDialog : WindowEx
{
    public SettingsDialog()
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