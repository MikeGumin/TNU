using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TNU.Core.Models;
using TNU.Core.Services.FinishedEntry;

namespace TNU.Core.Views;

public partial class EditEntryWindow : Window
{
    private readonly IFinishedEntryService _finishedEntryService;
    private readonly ErrorMessageHelper _errorMessageHelper;
    public JobEntry ResultEntry { get; private set; }

    public EditEntryWindow(
        JobEntry entryToEdit,
        IFinishedEntryService finishedEntryService,
        ErrorMessageHelper errorMessageHelper)
    {
        InitializeComponent();
        JobEntry entry = new JobEntry() 
        { 
            Id =  entryToEdit.Id,
            JobName = entryToEdit.JobName,
            StartTime= entryToEdit.StartTime,
            EndTime= entryToEdit.EndTime
        };
        
        DataContext = entry;
        
        _finishedEntryService = finishedEntryService;
        _errorMessageHelper = errorMessageHelper;
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

    private async void DeleteButton_Click(object? sender, RoutedEventArgs e)
    {
        var deleteResult = _finishedEntryService.DeleteEntry((JobEntry)DataContext!);

        if (deleteResult.IsFailed)
        {
            await _errorMessageHelper.ShowErrorMessage("Ошибка при удалении записи", deleteResult.ErrorMessage, this);
        }
        
        Close(false);
    }
}