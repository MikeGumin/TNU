using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using TNU.Models;
using TNU.Services.EntryExport;

namespace TNU.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    //private decimal greeting = 0;
    //public decimal Greeting
    //{
    //    get => greeting;
    //    private set => this.RaiseAndSetIfChanged(ref greeting, value);
    //}

    /// <summary>
    /// Коллекция для хранения текущих записей
    /// </summary>
    public ObservableCollection<JobEntry> TimerList { get; private set; } = [];
    private readonly IEntryExportService _entryExportService;
    
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

    public MainWindowViewModel(
        IEntryExportService entryExportService)
    {
        _entryExportService = entryExportService;
    }

    /// <summary>
    /// Метод создания новой записи
    /// </summary>
    [RelayCommand]
    private void StartTimer()
    {
        TimerList.Add(new JobEntry());
    }

    /// <summary>
    /// Метод для экспорта завершенных задач
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExport))]
    private void ExportEntries()
    {
        IsExporting = true;
        try
        {
            _entryExportService.ExportEntry(TimerList);
        }
        finally
        {
            IsExporting = false;
        }
    }
    
    /// <summary>
    /// Метод проверки на возможность экспорта записей
    /// </summary>
    /// <returns></returns>
    private bool CanExport() => !_isExporting;
}