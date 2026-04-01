using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System;
using System.Diagnostics;
using TNU.Models;

namespace TNU.ViewModels;

public partial class JobEntryViewModel : ReactiveObject
{

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

    public JobEntryViewModel()
    {
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
}