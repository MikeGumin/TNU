using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using TNU.Models;
using TNU.Repository;
using TNU.Services.EntryExport;
using TNU.Services.FinishedEntry;
using TNU.Views;

namespace TNU.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// Коллекция для хранения текущих записей
    /// </summary>
    public ObservableCollection<JobEntryViewModel> TimerList { get; private set; } = [];
    
    public Window? MainWindow { get; set; }
    
    private readonly IEntryExportService _entryExportService;
    private readonly IFinishedEntryService _finishedEntryService;

    public MainWindowViewModel(IEntryExportService entryExportService, IFinishedEntryService finishedEntryService)
    {
        _entryExportService = entryExportService;
        _finishedEntryService = finishedEntryService;

    }

    /// <summary>
    /// Метод создания новой записи
    /// </summary>
    [RelayCommand]
    private void AddNewTask()
    {
        JobEntryViewModel model = new JobEntryViewModel(_finishedEntryService, this);
        GeneralUpdateTimer.AddEvent(model);

        if(!GeneralUpdateTimer.IsEnabled)
            GeneralUpdateTimer.StartTimer();

        TimerList.Add(model);
    }

    /// <summary>
    /// Метод для экспорта завершенных задач
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExport))]
    private void ExportEntries()
    {
        _entryExportService.ExportEntry(FinishedEntriesRepository.FinishedEntries);
    }
    
    /// <summary>
    /// Метод для сохранения завершенных задач
    /// </summary>
    [RelayCommand]
    private void DeleteEntries()
    {
        _finishedEntryService.DeleteEntry();
    }
    
    /// <summary>
    /// Метод для редактирования завершенной записи
    /// </summary>
    /// <param name="entry">Запись для редактирования</param>
    [RelayCommand]
    private async Task EditEntry(JobEntry entry)
    {
        // Заносим данные из записи entry в наше окно для редактирования
        var editWindow = new EditEntryWindow(entry);
        
        // Вызываем диалог, где владельцем является наше главное окно
        // owner нужен, чтобы позиционировать наше всплывающее окно относительного главного
        bool? result = await editWindow.ShowDialog<bool?>(MainWindow!);
        
        // result - это флаг указывающий на то, была ли нажата кнопка "ок" или нет
        if (result == true)
        {
            var updatedEntry = _finishedEntryService.EditEntry(editWindow.ResultEntry, entry);
            
            var indexEditEntry = FinishedEntriesRepository.FinishedEntries.IndexOf(entry);
            
            if (indexEditEntry >= 0)
            {
                FinishedEntriesRepository.FinishedEntries[indexEditEntry] = updatedEntry;
            }
        }
    }

    /// <summary>
    /// Флаг для указания возможности экспорта записей
    /// </summary>
    private bool _isExporting = false;
    public bool IsExporting
    {
        get => _isExporting;
        private set
        {
            _isExporting = value;
            ExportEntriesCommand.NotifyCanExecuteChanged();
        }
    }

    /// <summary>
    /// Метод проверки на возможность экспорта записей
    /// </summary>
    /// <returns></returns>
    private bool CanExport() => !_isExporting;
}