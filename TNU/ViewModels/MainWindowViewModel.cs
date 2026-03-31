using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TNU.Models;
using TNU.Services.EntryExport;
using TNU.Services.EntrySave;

namespace TNU.ViewModels;

delegate void ReDrowTimerStr(object? sender, EventArgs e);

public partial class MainWindowViewModel : ViewModelBase
{

    /// <summary>
    /// Переменная для обновления отображаемого времени
    /// </summary>
    private DispatcherTimer _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };

    ReDrowTimerStr dl;

    /// <summary>
    /// Коллекция для хранения текущих записей
    /// </summary>
    public ObservableCollection<JobEntryViewModel> TimerList { get; private set; } = [];


    /// <summary>
    /// Коллекция для хранения завершенных записей
    /// </summary>
    public static ObservableCollection<JobEntry> FinishedEntries { get; private set; } = new();

    private readonly IEntryExportService _entryExportService;
    private readonly IEntrySaveService _entrySaveService;

    public MainWindowViewModel(IEntryExportService entryExportService, IEntrySaveService entrySaveService)
    {
        _entryExportService = entryExportService;
        _entrySaveService = entrySaveService;

        //___________________________________________________
        _timer.Tick += (_, __) =>
        {
            dl?.Invoke(_, __);

            System.Diagnostics.Debug.WriteLine("Work!!!");
        };
        //___________________________________________________

    }

    /// <summary>
    /// Метод создания новой записи
    /// </summary>
    [RelayCommand]
    private void AddNewTask()
    {
        var a = new JobEntryViewModel();
        //___________________________________________________
        if(!_timer.IsEnabled)
            _timer.Start();
        dl += a.Timer.ReDrowTimer;
        //___________________________________________________

        TimerList.Add(a);
    }

    /// <summary>
    /// Метод для экспорта завершенных задач
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExport))]
    private void ExportEntries()
    {
        _entryExportService.ExportEntry(FinishedEntries);
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

        //___________________________________________________

        if (TimerList.Count <= 0)
            _timer.Stop();
        //___________________________________________________

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