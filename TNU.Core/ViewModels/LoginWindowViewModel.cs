using System;
using CommunityToolkit.Mvvm.Input;
using TNU.Core.Models;

namespace TNU.Core.ViewModels
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
