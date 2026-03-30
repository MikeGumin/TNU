using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;

namespace TNU.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private decimal greeting = 0;
    public decimal Greeting
    {
        get => greeting;
        private set => this.RaiseAndSetIfChanged(ref greeting, value);
    }
    ObservableCollection<double> TimerList { get; set; } = [];

    //  this.RaiseAndSetIfChanged(ref _Имя_свойства, value)

    public DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(0.01) };
    #region Command

    public ReactiveCommand<Unit, Unit> AddNewTimer { get; set; }

    #endregion


    public MainWindowViewModel()
    {
        timer.Tick += (_, __) =>
        {
            Greeting += 0.01M;
        };


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
        if (!timer.IsEnabled)
            timer.Start();
        else
            timer.Stop();
    }
    #endregion
}