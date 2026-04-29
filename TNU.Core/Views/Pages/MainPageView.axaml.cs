using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TNU.Core.Models;
using TNU.Core.Repository;
using TNU.Core.Services.CsvFile;

namespace TNU.Core;

public partial class MainPageView : UserControl
{
    public MainPageView()
    {
        InitializeComponent();

        this.Loaded += (sender, e) =>
        {
            if (DataContext is ViewModels.MainWindow.MainWindowViewModel vm)
            {
                vm.MainUserControl = this;
            }
        };
    }

    private void JobCodeTextBox_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox && textBox.DataContext is JobEntryClock vm)
        {
            var entry = vm.Entry;

            if (!string.IsNullOrWhiteSpace(entry.JobName) && !string.IsNullOrWhiteSpace(entry.JobCode))
            {
                JobNameRepository.JobNameCodeList[entry.JobName] = entry.JobCode;
                ReadCsvFile.DeleteEntry(entry.JobName, SystemConst.JobNameFilePath);
                ReadCsvFile.Write($"{entry.JobName}:{entry.JobCode}");
            }
        }
    }

    private void JobCodeTextBox_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        if (sender is TextBox textBox && textBox.DataContext is JobEntryClock vm)
        {
            var entry = vm.Entry;

            if (!string.IsNullOrWhiteSpace(entry.JobName) && JobNameRepository.JobNameCodeList.TryGetValue(entry.JobName, out var code))
            {
                textBox.Text = code;
            }
        }
    }


}