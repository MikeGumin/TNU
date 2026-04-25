using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using TNU.Core.Models;
using TNU.Core.Repository;
using TNU.Core.Services.CloseWindow;
using TNU.Core.Services.EntryExport;
using TNU.Core.Services.FileDialog;
using TNU.Core.ViewModels.MainWindow;
using TNU.Core.Views;

namespace TNU.Core.ViewModels;

/// <summary>
/// Логика окна с сохраненными записями за весь ФРД
/// </summary>
public partial class FrdWindowViewModel: ViewModelBase
{
    private readonly IEntryExportService _entryExportService;
    private readonly IFileDialogService _fileDialogService;
    private readonly ErrorMessageHelper _errorMessageHelper;
    private readonly IWindowService _windowService;
    private int frdId = 1;
    public Observation ObservationElement { get; set; } = new Observation();
    
    public Window? FrdWindow { get; set; }
    
    public FrdWindowViewModel(
        IEntryExportService entryExportService,
        IFileDialogService fileDialogService, 
        ErrorMessageHelper errorMessageHelper,
        IWindowService windowService)
    {
        _entryExportService = entryExportService;
        _fileDialogService = fileDialogService;
        _errorMessageHelper = errorMessageHelper;
        _windowService = windowService;
    }
    
    /// <summary>
    /// Метод для экспорта завершенных задач
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExport))]
    private async Task ExportEntries()
    {
        var exportResult = await _entryExportService.CsvEntryAsync(
            FinishedEntriesRepository.FinishedEntries,
            _fileDialogService
        );

        if (exportResult.IsFailed)
        {
            await _errorMessageHelper.ShowErrorMessage("Ошибка экспорта файлов", exportResult.ErrorMessage, FrdWindow!);
        }
    }

    /// <summary>
    /// Метод для экспорта завершенных задач в формате Диаграммы Ганта
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExport))]
    private async Task ExportEntriesInGanta()
    {
        var exportResult = await _entryExportService.ExportDiagrammaGanta(FinishedEntriesRepository.FinishedEntries, _fileDialogService);
        
        if (exportResult.IsFailed)
        {
            await _errorMessageHelper.ShowErrorMessage("Ошибка экспорта файлов", exportResult.ErrorMessage, FrdWindow!);
        }
    }
    
    #region Проверка на возможность экспорта

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

    #endregion
    
    private Observation _mainObservation;
    public Observation MainObservation
    {
        get => _mainObservation;
        set
        {
            if (_mainObservation == null)
            {
                _mainObservation = value;
                OnPropertyChanged();
                //_finishedEntryService.FinishedEntries = value.FinishedEntries;
            }
        }
    }
    
    /// <summary>
    /// Метод закрытия окна ФРД и возвращения к основному окну приложения 
    /// </summary>
    [RelayCommand]
    private void CloseFrdWindow()
    {
        var mainWindow = new Views.MainWindow()
        {
            DataContext = App.Services.GetRequiredService<MainWindowViewModel>()
        };

        if (mainWindow.DataContext is MainWindowViewModel a)
        {
            a.MainObservation = ObservationElement;
        }

        mainWindow.Show();

        _windowService.CloseCurrentWindow();
    }

    [RelayCommand]
    private void GoToListFrd()
    {
        var files = Directory.EnumerateFiles(@".", "output*", SearchOption.AllDirectories);
        
        FrdRepository.FinishedFrd.Clear();

        foreach (var file in files)
        {
            FrdRepository.FinishedFrd.Add( new FrdModel()
            {
                Id = frdId++,
                FileName = Path.GetFileName(file),
            });
        }
        
        var frdWindow = new Views.AllFrdWindow()
        {
            DataContext = App.Services.GetRequiredService<AllFrdWindowViewModel>()
        };

        if (frdWindow.DataContext is AllFrdWindowViewModel a)
        {
            a.MainObservation = ObservationElement;
        }

        frdWindow.Show();

        _windowService.CloseCurrentWindow();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}