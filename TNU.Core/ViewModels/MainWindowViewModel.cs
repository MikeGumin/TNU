using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using TNU.Core.Models;
using TNU.Core.Repository;
using TNU.Core.Services;
using TNU.Core.Services.EntryExport;
using TNU.Core.Services.FileDialog;
using TNU.Core.Services.FinishedEntry;
using EditEntryWindow = TNU.Core.Views.EditEntryWindow;

namespace TNU.Core.ViewModels;


public partial class MainWindowViewModel : ViewModelBase
{
    /// <summary>
    /// Коллекция для хранения текущих записей
    /// </summary>
    public ObservableCollection<Core.ViewModels.JobEntryViewModel> TimerList { get; private set; } = [];

    public Observation observation { get; private set; } = new();

    public Window? MainWindow { get; set; }
    
    private readonly IEntryExportService _entryExportService;
    private readonly IFinishedEntryService _finishedEntryService;
    private readonly IFileDialogService _fileDialogService;
    private readonly ErrorMessageHelper _errorMessageHelper;

    public MainWindowViewModel(
        IEntryExportService entryExportService,
        IFinishedEntryService finishedEntryService,
        IFileDialogService fileDialogService,
        ErrorMessageHelper errorMessageHelper)
    {
        _entryExportService = entryExportService;
        _finishedEntryService = finishedEntryService;
        _fileDialogService = fileDialogService;
        _errorMessageHelper = errorMessageHelper;
    }

    /// <summary>
    /// Метод создания новой записи
    /// </summary>
    [RelayCommand]
    private async Task AddNewTask()
    {
        Core.ViewModels.JobEntryViewModel model = new Core.ViewModels.JobEntryViewModel(_finishedEntryService, this);

        model.Entry = new JobEntry();
        
        GeneralUpdateTimer.AddEvent(model);

        if (!GeneralUpdateTimer.IsEnabled)
        {
            GeneralUpdateTimer.StartTimer();
        }

        TimerList.Add(model);
        
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
            await _errorMessageHelper.ShowErrorMessage("Ошибка экспорта файлов", exportResult.ErrorMessage, MainWindow!);
        }
    }
    
    /// <summary>
    /// Метод для экспорта завершенных задач в формате Диаграммы Ганта
    /// </summary>
    [RelayCommand(CanExecute = nameof(CanExport))]
    private async Task ExportEntriesInGanta()
    {
        // var exportResult = await _entryExportService.ExportDiagrammaGanta(FinishedEntriesRepository.FinishedEntries, _fileDialogService);
        //
        // if (exportResult.IsFailed)
        // {
        //     await _errorMessageHelper.ShowErrorMessage("Ошибка экспорта файлов", exportResult.ErrorMessage, MainWindow!);
        // }
    }
    
    /// <summary>
    /// Метод для удаления всех завершенных задач
    /// </summary>
    [RelayCommand]
    private async Task DeleteEntries()
    {
        var deleteResult = _finishedEntryService.DeleteEntries();

        if (deleteResult.IsFailed)
        {
            await _errorMessageHelper.ShowErrorMessage("Ошибка при удалении записей", deleteResult.ErrorMessage, MainWindow!);
        }
    }
    
    /// <summary>
    /// Метод для удаления завершенной задачи
    /// </summary>
    [RelayCommand]
    private async Task DeleteEntry(JobEntry entry)
    {
        
    }
    
    /// <summary>
    /// Метод для редактирования завершенной записи
    /// </summary>
    /// <param name="entry">Запись для редактирования</param>
    [RelayCommand]
    private async Task EditEntry(JobEntry entry)
    {
        // Заносим данные из записи entry в наше окно для редактирования
        EditEntryWindow editWindow = new EditEntryWindow(entry, _finishedEntryService, _errorMessageHelper);

        // Вызываем диалог, где владельцем является наше главное окно
        // owner нужен, чтобы позиционировать наше всплывающее окно относительного главного
        bool? result = await editWindow.ShowDialog<bool?>(MainWindow!);
        
        // result - это флаг указывающий на то, была ли нажата кнопка "ок" или нет
        if (result == true)
        {
            //JobEntry updatedEntry =
             var editResult = _finishedEntryService.EditEntry(editWindow.ResultEntry, entry);

             if (editResult.IsFailed)
             {
                 await _errorMessageHelper.ShowErrorMessage("Ошибка при редактировании записи", editResult.ErrorMessage, MainWindow!);
                 editWindow.Close();
             }

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