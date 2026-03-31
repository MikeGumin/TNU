using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TNU.Models;
using TNU.Services.EntryExport;

namespace TNU.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// Коллекция для хранения текущих записей
    /// </summary>
    public ObservableCollection<JobEntryViewModel> TimerList { get; private set; } = [];
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
        TimerList.Add(new JobEntryViewModel());
    }

    /// <summary>
    /// Метод для экспорта завершенных задач
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExport))]
    private void ExportEntries()
    {
        _entryExportService.ExportEntry(TimerList.Select(vm => vm.Entry));
    }
    
    /// <summary>
    /// Метод проверки на возможность экспорта записей
    /// </summary>
    /// <returns></returns>
    private bool CanExport() => !_isExporting;
}