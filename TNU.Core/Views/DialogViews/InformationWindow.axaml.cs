using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace TNU.Core.Views.DialogViews;

public partial class InformationWindow : Window
{
    public InformationWindow()
    {
        InitializeComponent();
    }
    
    private void OnYes_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}