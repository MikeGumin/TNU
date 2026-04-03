using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using TNU.Models;
using TNU.Repository;
using TNU.Services;
using TNU.Services.EntryExport;
using TNU.Services.FileDialog;
using TNU.Services.FinishedEntry;
using TNU.Views;

namespace TNU.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// Коллекция для хранения текущих записей
    /// </summary>
    public ObservableCollection<JobEntryViewModel> TimerList { get; private set; } = [];

    public Observation observation { get; private set; } = new();

    public Window? MainWindow { get; set; }
    
    private readonly IEntryExportService _entryExportService;
    private readonly IFinishedEntryService _finishedEntryService;
    private readonly IFileDialogService _fileDialogService;

    public MainWindowViewModel(
        IEntryExportService entryExportService,
        IFinishedEntryService finishedEntryService,
        IFileDialogService fileDialogService)
    {
        _entryExportService = entryExportService;
        _finishedEntryService = finishedEntryService;
        _fileDialogService = fileDialogService;
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
    private async Task ExportEntries()
    {
        await _entryExportService.ExportEntryAsync(
            FinishedEntriesRepository.FinishedEntries,
            _fileDialogService
        );
    }
    
    /// <summary>
    /// Метод для экспорта завершенных задач в формате Диаграммы Ганта
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExport))]
    private void ExportEntriesInGanta()
    {
        _entryExportService.ExportDiagrammaGanta(FinishedEntriesRepository.FinishedEntries);
    }
    
    /// <summary>
    /// Метод для удаления всех завершенных задач
    /// </summary>
    [RelayCommand]
    private void DeleteEntries()
    {
        _finishedEntryService.DeleteEntries();
    }
    
    /// <summary>
    /// Метод для удаления завершенной задачи
    /// </summary>
    [RelayCommand]
    private void DeleteEntry(JobEntry entry)
    {
        _finishedEntryService.DeleteEntry(entry);
    }
    
    /// <summary>
    /// Метод для редактирования завершенной записи
    /// </summary>
    /// <param name="entry">Запись для редактирования</param>
    [RelayCommand]
    private async Task EditEntry(JobEntry entry)
    {
        // Заносим данные из записи entry в наше окно для редактирования
        EditEntryWindow editWindow = new EditEntryWindow(entry);

        // Вызываем диалог, где владельцем является наше главное окно
        // owner нужен, чтобы позиционировать наше всплывающее окно относительного главного
        bool? result = await editWindow.ShowDialog<bool?>(MainWindow!);
        
        // result - это флаг указывающий на то, была ли нажата кнопка "ок" или нет
        if (result == true)
        {
             _finishedEntryService.EditEntry(editWindow.ResultEntry, entry);

            //int indexEditEntry = FinishedEntriesRepository.FinishedEntries.IndexOf(entry);
            
            //if (indexEditEntry >= 0)
            //{
            //    FinishedEntriesRepository.FinishedEntries[indexEditEntry] = updatedEntry;
            //}
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