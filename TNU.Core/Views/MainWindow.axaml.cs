using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using TNU.Core.Repository;
using TNU.Core.Services.CsvFile;
using TNU.Core.ViewModels;
using MainWindowViewModel = TNU.Core.ViewModels.MainWindowViewModel;

namespace TNU.Core.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Нужно для передачи в контекст родительского окна
        this.Loaded += (sender, e) =>
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.MainWindow = this;
            }
        };
    }

    private void JobCodeTextBox_OnLostFocus(object? sender, RoutedEventArgs e)
    {
        if (sender is TextBox textBox && textBox.DataContext is JobEntryViewModel vm)
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
        if (sender is TextBox textBox && textBox.DataContext is JobEntryViewModel vm)
        {
            var entry = vm.Entry;
            
            if (!string.IsNullOrWhiteSpace(entry.JobName) && JobNameRepository.JobNameCodeList.TryGetValue(entry.JobName, out var code))
            {
                textBox.Text = code;
            } 
        } 
    }
}