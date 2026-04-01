using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using TNU.Models;
<<<<<<< HEAD
using TNU.Services.ClockAction;
=======
using TNU.Services.FinishedEntry;
>>>>>>> 7dddf085bfba694189d281c5767f033f49c8b3ef

namespace TNU.ViewModels;

public partial class JobEntryViewModel : ReactiveObject
{
<<<<<<< HEAD
=======
    private readonly IFinishedEntryService _finishedEntryService;
>>>>>>> 7dddf085bfba694189d281c5767f033f49c8b3ef

    /// <summary>
    /// Переменная для отсчета времени
    /// </summary>
<<<<<<< HEAD
    public ClockActionService Timer { get; } = new ClockActionService();
=======
    public Clock Timer { get; } = new Clock();
>>>>>>> 7dddf085bfba694189d281c5767f033f49c8b3ef

    /// <summary>
    /// Данные модели
    /// </summary>
<<<<<<< HEAD
=======
    //private DispatcherTimer _timer = new DispatcherTimer
    //{
    //    Interval = TimeSpan.FromMilliseconds(10)
    //};

    /// <summary>
    /// Данные модели
    /// </summary>
>>>>>>> 7dddf085bfba694189d281c5767f033f49c8b3ef
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

<<<<<<< HEAD
=======
        //_timer.Tick += Timer.ReDrowTimer;

>>>>>>> 7dddf085bfba694189d281c5767f033f49c8b3ef
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

<<<<<<< HEAD
=======

>>>>>>> 7dddf085bfba694189d281c5767f033f49c8b3ef
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