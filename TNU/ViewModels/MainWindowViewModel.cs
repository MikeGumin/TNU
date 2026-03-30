using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tmds.DBus.Protocol;

namespace TNU.ViewModels;

public partial class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    public string Greeting { get; private set; } = "dfgdfgdfgdfg";
    List<double> TimerList { get; set; }


    public MainWindowViewModel()
    {
        OnPropertyChanged("Greeting");
        Greeting = "1123";
    }
    public void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Greeting = "eeeeeeeeeeeeeee";
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}