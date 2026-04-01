using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System;
using System.Diagnostics;
using TNU.Models;
using TNU.Services.FinishedEntry;

namespace TNU.ViewModels;

public partial class JobEntryViewModel : ReactiveObject
{
    private readonly IFinishedEntryService _finishedEntryService;

    /// <summary>
    /// Переменная для отсчета времени
    /// </summary>
    public Clock Timer { get; } = new Clock();

    /// <summary>
    /// Переменная для обновления отображаемого времени
    /// </summary>
    //private DispatcherTimer _timer = new DispatcherTimer
    //{
    //    Interval = TimeSpan.FromMilliseconds(10)
    //};

    /// <summary>
    /// Данные модели
    /// </summary>
    public JobEntry Entry { get; set; } = new();

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="finishedEntryService">Сервис для работы с завершенными записями</param>
    public JobEntryViewModel(
        IFinishedEntryService finishedEntryService)
    {
        _finishedEntryService = finishedEntryService;
        Entry.JobWorker = new Worker() { FullName = "test" };

        //_timer.Tick += Timer.ReDrowTimer;

        StartTimer();
    }

    public void StartTimer()
    {
        Timer.StartTimer();
        Entry.RecordStatus = RecordStatusEnum.Start;
    }

    [RelayCommand]
    public void StopTimer()
    {
        Timer.StopTimer();
        Entry.RecordStatus = RecordStatusEnum.Stop;
    }

    [RelayCommand]
    public void EndTimer()
    {
        if(Entry.RecordStatus!= RecordStatusEnum.Stop)
            StopTimer();


        Entry.JobSample = Timer.StrTimer;
        Entry.RecordStatus = RecordStatusEnum.Finish;

        //System.Diagnostics.Debug.WriteLine(Entry.JobSample);
    }
    
    /// <summary>
    /// Метод для сохранения завершенных задач
    /// </summary>
    [RelayCommand]
    private void EditEntry()
    {
        Entry = _finishedEntryService.EditEntry(Entry);
    }
}