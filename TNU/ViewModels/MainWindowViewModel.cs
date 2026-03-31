using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using TNU.Models;

namespace TNU.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private decimal greeting = 0;
    public decimal Greeting
    {
        get => greeting;
        private set => this.RaiseAndSetIfChanged(ref greeting, value);
    }
    public ObservableCollection<JobEntry> TimerList { get; set; } = [];

    //  this.RaiseAndSetIfChanged(ref _Имя_свойства, value)

    //public DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.01) };
    #region Command

    public ReactiveCommand<Unit, Unit> AddNewTimer { get; set; }

    #endregion


    public MainWindowViewModel()
    {
        //timer.Tick += (_, __) =>
        //{
        //    Greeting += 0.01M;
        //};


        //AddNewTimer = ReactiveCommand.Create(() =>
        //{
        //    Console.WriteLine("Hello!");
        //    //timer.Start();
        //});

    }

    #region test

    [RelayCommand]
    private void StartTimer()
    {
        TimerList.Add(new JobEntry());


        //TimerList.Add(new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.01) });
        //TimerStringList.Add(new decimal(0));
        //TimerList.Last().Tick += (_, __) =>
        //{
        //    TimerStringList[TimerStringList.Count-1] += 0.01M;
        //};

        //TimerList[TimerList.Count-1].Tick +=

        //if (!timer.IsEnabled)
        //    timer.Start();
        //else
        //    timer.Stop();
    }
    #endregion
}