using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using TNU.Models;
using TNU.Repository;
using TNU.Services.EntryExport;
using TNU.Services.EntrySave;

namespace TNU.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// Коллекция для хранения текущих записей
    /// </summary>
    public ObservableCollection<JobEntryViewModel> TimerList { get; private set; } = [];

    private readonly IEntryExportService _entryExportService;
    private readonly IEntrySaveService _entrySaveService;

    public MainWindowViewModel(IEntryExportService entryExportService, IEntrySaveService entrySaveService)
    {
        _entryExportService = entryExportService;
        _entrySaveService = entrySaveService;
    }

    /// <summary>
    /// Метод создания новой записи
    /// </summary>
    [RelayCommand]
    private void AddNewTask()
    {
        JobEntryViewModel model = new JobEntryViewModel();
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
    private void SaveEntries()
    {
        _entrySaveService.SaveEntry(TimerList.Select(vm => vm.Entry));

        var toRemove = TimerList.Where(vm => vm.Entry.RecordStatus == RecordStatusEnum.Finish).ToList();
        foreach (var vm in toRemove)
        {
            TimerList.Remove(vm);
        }

        if (TimerList.Count <= 0) GeneralUpdateTimer.StopTimer();
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