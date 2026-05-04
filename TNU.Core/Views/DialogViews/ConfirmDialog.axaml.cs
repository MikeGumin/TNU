using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TNU.Core.Views.DialogViews;

public partial class ConfirmDialog : Window
{
    public bool Result { get; private set; }

    public ConfirmDialog(string message)
    {
        InitializeComponent();
        MessageText.Text = message;
        DataContext = this;
    }
    
    
    private void OnYes_Click(object sender, RoutedEventArgs e)
    {
        Result = true;
        Close();
    }

    private void OnNo_Click(object sender, RoutedEventArgs e)
    {
        Result = false;
        Close();
    }
}