using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using TNU.Core.Models;
using TNU.Core.Models.Enum;
using TNU.Core.Services;
using TNU.Core.Services.CloseWindow;
using TNU.Core.Services.CsvFile;
using TNU.Core.Services.EntryExport;
using TNU.Core.Services.FileDialog;
using TNU.Core.Services.FinishedEntry;
using EditEntryWindow = TNU.Core.Views.EditEntryWindow;

namespace TNU.Core.ViewModels.MainWindow;


public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    /// <summary>
    /// Пааметр видимости комментария
    /// </summary>
    private bool _isVisible = false;
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            _isVisible = value;
            OnPropertyChanged();
            //this.RaiseAndSetIfChanged(ref _isVisible, value);
        }
    }


    /// <summary>
    /// массив для заготовок
    /// </summary>
    public ObservableCollection<JobEntryClock> ListPreparation { get; private set; } = [];

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

    #region Readonly поля и кнструктор

    public Window? MainWindow { get; set; }
    public UserControl? MainUserControl { get; set; }

    private readonly IEntryExportService _entryExportService;
    private readonly IFinishedEntryService _finishedEntryService;
    private readonly IFileDialogService _fileDialogService;
    private readonly IWindowService _windowService;
    private readonly ErrorMessageHelper _errorMessageHelper;


    public MainWindowViewModel(
        IEntryExportService entryExportService,
        IFinishedEntryService finishedEntryService,
        IFileDialogService fileDialogService,
        IWindowService windowService,
        ErrorMessageHelper errorMessageHelper)
    {
        _entryExportService = entryExportService;
        _finishedEntryService = finishedEntryService;
        _fileDialogService = fileDialogService;
        _windowService = windowService;
        _errorMessageHelper = errorMessageHelper;

        SystemStatic.GeneralStopwatch.Stop();
    }

    #endregion

    /// <summary>
    /// Метод создания новой записи
    /// </summary>
    [RelayCommand]
    private async Task AddNewTask()
    {
        JobEntryClock model = MainObservation.AddToActivListR();

        File.AppendAllLines(SystemStatic.EntryFilePath, new[] { model.Entry.Id.ToString() });

        GeneralUpdateTimer.AddEvent(model);

        if (!GeneralUpdateTimer.IsEnabled)
        {
            SystemStatic.GeneralStopwatch.Start();
            GeneralUpdateTimer.StartTimer();
        }
    }


    //----------------------------------------------------------------------------------------------------------------------
    
    /// <summary>
    /// Добаление новой задачи в ListPreparation (Массив заготовок задач)
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    public async Task AddNewTaskForListPreparation()
    {
        var model = new JobEntryClock();

        ListPreparation.Add(model);
    }

    /// <summary>
    /// Добавление новой задачи из Массива заготовок в активный лист
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [RelayCommand]
    public void AddTaskForomListPreparation(object obj)
    {
        if (obj is JobEntryClock j)
        {
            JobEntryClock model = MainObservation.AddToActivListR(j.Entry.JobName);
            model.Entry.JobCode = j.Entry.JobCode;

            File.AppendAllLines(SystemStatic.EntryFilePath, new[] { model.Entry.Id.ToString() });
            
            if (!j.IsSavePrepareJob)
            {
                DeliteFromListPreparation(j);
            }
            
            GeneralUpdateTimer.AddEvent(model);

            if (!GeneralUpdateTimer.IsEnabled)
            {
                SystemStatic.GeneralStopwatch.Start();
               // SystemStatic.GeneralStopwatch2 = DateTime.Now;
                GeneralUpdateTimer.StartTimer();
            }
        }
    }

    /// <summary>
    /// Удаление задачи из листа заготовок
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    [RelayCommand]
    public void DeliteFromListPreparation(object obj)
    {
        if (obj is JobEntryClock j)
        {
            ListPreparation.Remove(j);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------


    //----------------------------------------------------------------------------------------------------------------------

    public void StartTimer(object obj)
    {
        if (obj is JobEntryClock j)
            TimerControlService.StartTimer(j);
    }

    public void StopTimer(object obj)
    {
        if (obj is JobEntryClock j)
            TimerControlService.StopTimer(j);
    }

    [RelayCommand]
    public void ChangeTimer(object obj)
    {
        if (obj is JobEntryClock j)
        {
            TimerControlService.ChangeTimer(j);
            j.ChangeBtnText();
        }
    }


    [RelayCommand]
    public void CommentVisibility()
    {
        IsVisible = !IsVisible;
    }

    [RelayCommand]
    public void EndTimer(object obj)
    {
        if (obj is JobEntryClock jobModel)
        {
            TimerControlService.EndTimer(jobModel);

            jobModel.Entry.JobSample = jobModel.Timer.StrTimer;
            jobModel.Entry.RecordStatus = RecordStatusEnum.Finish;
            jobModel.Entry.IsTimedCorrectly = jobModel.IsSavePrepareJob;

            _finishedEntryService.SaveEntry(new List<JobEntry>() { jobModel.Entry });
            MainObservation.JobEntriesActiv.Remove(jobModel);

            ReadCsvFile.DeleteEntry(jobModel.Entry.Id.ToString(), SystemStatic.EntryFilePath);
            ReadCsvFile.WriteJobInFile(jobModel.Entry, SystemStatic.EntryFilePath);
        }
    }
    //----------------------------------------------------------------------------------------------------------------------
    
    
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

    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}