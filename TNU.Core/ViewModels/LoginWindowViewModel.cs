using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using TNU.Core.Models;
using TNU.Core.Services;
using TNU.Core.Services.CloseWindow;
using TNU.Core.Services.FileOpener;

namespace TNU.Core.ViewModels
{
    public partial class LoginWindowViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService;
        public Observation ObservationElement { get; set; } = new Observation();

        public LoginWindowViewModel(IWindowService windowService)
        {
            _windowService = windowService;
        }
        public LoginWindowViewModel()
        {
            _windowService = new WindowService();
        }

        [RelayCommand]
        public void Login()
        {
            // Проверка введены ли данные в окна. Работает, но пока убрал 
            //if (ObservationElement.IsCompleted())
                OnLoginSuccess();
        }

        [RelayCommand]
        public void OpenFile()
        {
            FileOpenerServise file = new FileOpenerServise();
            file.OpenFile();
        }

        private void OnLoginSuccess()
        {
            var mainWindow = new Core.Views.MainWindow
            {
                DataContext = App.Services.GetRequiredService<MainWindowViewModel>()
            };
            mainWindow.Show();

            if (mainWindow.DataContext is MainWindowViewModel a)
            {
                a.MainObservation = ObservationElement;
            }
            _windowService.CloseCurrentWindow();
        }
        //if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)




    }
}
