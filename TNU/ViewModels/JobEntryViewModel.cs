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
    private readonly Stopwatch _stopwatch = new Stopwatch();

    /// <summary>
    /// Переменная для обновления отображаемого времени
    /// </summary>
    private DispatcherTimer _timer = new DispatcherTimer
    {
        Interval = TimeSpan.FromMilliseconds(10)
    };

    // Данные модели
    public JobEntry Entry { get; } = new();
    
    public Worker JobWorker => Entry.JobWorker;
    public string JobName
    {
        get => Entry.JobName;
        set => Entry.JobName = value;
    }
    public RecordStatusEnum RecordStatus => Entry.RecordStatus;

    /// <summary>
    /// Таймер для отображения
    /// </summary>
    private string _jobTimer = "";
    public string JobTimer
    {
        get => _jobTimer;
        private set => this.RaiseAndSetIfChanged(ref _jobTimer, value);
    }

    public JobEntryViewModel()
    {
        Entry.JobWorker = new Worker() { FullName = "test" };

        _timer.Tick += (_, __) =>
        {
            TimeSpan elapsed = _stopwatch.Elapsed;
            JobTimer = $"{elapsed.Minutes:00}:{elapsed.Seconds:00}:{elapsed.Milliseconds:000}";
        };

        StartTimer();
    }

    public void StartTimer()
    {
        _stopwatch.Start();
        _timer.Start();
        Entry.RecordStatus = RecordStatusEnum.Start;
    }

    [RelayCommand]
    public void StopTimer()
    {
        _timer.Stop();
        _stopwatch.Stop();
        Entry.RecordStatus = RecordStatusEnum.Stop;
    }

    [RelayCommand]
    public void EndTimer()
    {
        Entry.JobSample = _stopwatch.Elapsed.ToString().Split(".")[0];
        Entry.RecordStatus = RecordStatusEnum.Finish;
    }
}