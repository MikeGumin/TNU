﻿using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using TNU.Models;
using TNU.Repository;
using TNU.Services.EntryExport;
using TNU.Services.FinishedEntry;

namespace TNU.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{


    /// <summary>
    /// Коллекция для хранения текущих записей
    /// </summary>
    public ObservableCollection<JobEntryViewModel> TimerList { get; private set; } = [];

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
        JobEntryViewModel model = new JobEntryViewModel(_finishedEntryService);
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
        _finishedEntryService.SaveEntry(TimerList.Select(vm => vm.Entry));

        var toRemove = TimerList.Where(vm => vm.Entry.RecordStatus == RecordStatusEnum.Finish).ToList();
        foreach (var vm in toRemove)
        {
            TimerList.Remove(vm);
        }

        if (TimerList.Count <= 0) GeneralUpdateTimer.StopTimer();
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