using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using TNU.Models;
using TNU.Services.ClockAction;

namespace TNU.ViewModels;

public partial class JobEntryViewModel : ReactiveObject
{

    /// <summary>
    /// Переменная для отсчета времени
    /// </summary>
    public ClockActionService Timer { get; } = new ClockActionService();

    /// <summary>
    /// Данные модели
    /// </summary>
    public JobEntry Entry { get; set; } = new();

    public JobEntryViewModel()
    {
        Entry.JobWorker = new Worker() { FullName = "test" };

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
    }
}