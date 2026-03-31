using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using TNU.Models;

namespace TNU.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    //private decimal greeting = 0;
    //public decimal Greeting
    //{
    //    get => greeting;
    //    private set => this.RaiseAndSetIfChanged(ref greeting, value);
    //}
    public ObservableCollection<JobEntry> TimerList { get; private set; } = [];

    public MainWindowViewModel()
    {
    }

    [RelayCommand]
    private void StartTimer()
    {
        TimerList.Add(new JobEntry());
    }
}