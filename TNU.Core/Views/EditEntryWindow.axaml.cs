using Avalonia.Controls;
using Avalonia.Interactivity;
using TNU.Core.Models;

namespace TNU.Core.Views;

public partial class EditEntryWindow : Window
{
    public JobEntry ResultEntry { get; private set; }

    public EditEntryWindow(JobEntry entryToEdit)
    {
        InitializeComponent();
        JobEntry entry = new JobEntry() 
        { 
            JobName = entryToEdit.JobName,
            StartTime= entryToEdit.StartTime,
            EndTime= entryToEdit.EndTime
        };

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
}