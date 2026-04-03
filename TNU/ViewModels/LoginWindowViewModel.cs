using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using TNU.Models;

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
            LoginSuccess?.Invoke();
        }
    }
}
