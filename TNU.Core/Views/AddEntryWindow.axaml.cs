using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using TNU.Core.Models;

namespace TNU.Core.Views;

public partial class AddEntryWindow : Window
{
    public JobEntry ResultEntry { get; private set; }

    public AddEntryWindow()
    {
        InitializeComponent();
        
        JobEntry entry = new JobEntry();

        DataContext = entry;
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

    private void StartTimeDefault_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        StartTimeTextBox.Text = DateTime.Now.ToString("HH:mm:ss");
    }
}