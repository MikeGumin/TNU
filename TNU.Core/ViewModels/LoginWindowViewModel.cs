using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using TNU.Core.Models;
using TNU.Core.Services.FileOpener;

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

        [RelayCommand]
        public void OpenFile()
        {
            FileOpenerServise a = new FileOpenerServise();
            a.OpenFile();
        }


        

    }
}
