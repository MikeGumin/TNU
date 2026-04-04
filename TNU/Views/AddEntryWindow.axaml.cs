using Avalonia.Controls;
using Avalonia.Interactivity;
using TNU.Models;

namespace TNU.Views;

public partial class AddEntryWindow : Window
{
    public JobEntry ResultEntry { get; private set; }

    public AddEntryWindow()
    {
        InitializeComponent();
    }

    private void OkButton_Click(object? sender, RoutedEventArgs e)
    {
        ResultEntry = (JobEntry)DataContext!;
        Close(true);
    }

    private void CancelButton_Click(object? sender, RoutedEventArgs e)
    {
        Close(false);
    }
}