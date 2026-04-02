using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System.Collections.Generic;
using TNU.Models;
using TNU.Services.ClockAction;
using TNU.Services.FinishedEntry;

namespace TNU.ViewModels;

public partial class JobEntryViewModel : ReactiveObject
{
    private readonly IFinishedEntryService _finishedEntryService;
    private readonly MainWindowViewModel _parent;

    /// <summary>
    /// Переменная для отсчета времени
    /// </summary>
    public ClockActionService Timer { get; } = new ClockActionService();

    /// <summary>
    /// Данные модели
    /// </summary>
    public JobEntry Entry { get; set; } = new();

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="finishedEntryService">Сервис для работы с завершенными записями</param>
    public JobEntryViewModel(IFinishedEntryService finishedEntryService, MainWindowViewModel parent)
    {
        _finishedEntryService = finishedEntryService;
        _parent = parent;
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
        if (Entry.RecordStatus is RecordStatusEnum.Start)
        {
            Timer.StopTimer();
            Entry.RecordStatus = RecordStatusEnum.Stop;
        }
        else
        {
            Timer.StartTimer();
            Entry.RecordStatus = RecordStatusEnum.Start;
        }
        
    }

    [RelayCommand]
    public void EndTimer()
    {
        if(Entry.RecordStatus!= RecordStatusEnum.Stop)
            StopTimer();


        Entry.JobSample = Timer.StrTimer;
        Entry.RecordStatus = RecordStatusEnum.Finish;

        
        _finishedEntryService.SaveEntry(new List<JobEntry>() { Entry });
        _parent.TimerList.Remove(this);

        //System.Diagnostics.Debug.WriteLine(Entry.JobSample);
    }
}