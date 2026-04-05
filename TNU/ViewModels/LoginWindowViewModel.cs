using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using TNU.Models;
using TNU.Repository;

namespace TNU.ViewModels
{
    public partial class LoginWindowViewModel : ViewModelBase
    {
        public Observation ObservationElement { get; set; } = new Observation();

        public event Action? LoginSuccess;

        public LoginWindowViewModel()
        {

        }

        [RelayCommand]
        public void Login()
        {
            MainObservationElement.ObservationElement = ObservationElement;
            LoginSuccess?.Invoke();
        }
    }
}
