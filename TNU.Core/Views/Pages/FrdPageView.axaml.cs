using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using TNU.Core.Models;
using TNU.Core.Repository;
using TNU.Core.Services.CsvFile;

namespace TNU.Core;

public partial class FrdPageView : UserControl
{
    public FrdPageView()
    {
        InitializeComponent();
    }
    
    private void JobCodeTextBox_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (sender is TextBox textBox && textBox.DataContext is JobEntry entry)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text) &&
                    !string.IsNullOrWhiteSpace(entry.JobName) &&
                    JobNameRepository.JobNameCodeList.TryGetValue(entry.JobName, out var code) &&
                    !string.IsNullOrWhiteSpace(code))
                {
                    textBox.Text = code;
                }
            }
        });
    }

    private void JobCodeTextBox_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (sender is TextBox textBox && textBox.DataContext is JobEntry entry)
            {
                if (!string.IsNullOrWhiteSpace(entry.JobName) && 
                    !string.IsNullOrWhiteSpace(entry.JobCode))
                {
                    JobNameRepository.JobNameCodeList[entry.JobName] = entry.JobCode;
                    ReadCsvFile.DeleteEntry(entry.JobName, SystemConst.JobNameFilePath);
                    ReadCsvFile.Write($"{entry.JobName}:{entry.JobCode}");
                }
            }
        });
    }
}