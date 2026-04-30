using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using TNU.Core.Models;
using TNU.Core.Services;
using TNU.Core.Services.CloseWindow;
using TNU.Core.Services.FileOpener;
using MainWindowViewModel = TNU.Core.ViewModels.MainWindow.MainWindowViewModel;

namespace TNU.Core.ViewModels
{
    public partial class LoginWindowViewModel : ViewModelBase
    {
        private readonly IWindowService _windowService;
        private readonly IFileOpenerService _fileOpenerService;
        public Observation ObservationElement { get; set; } = new Observation();

        public LoginWindowViewModel()
        {
        }
        public LoginWindowViewModel(IWindowService windowService, IFileOpenerService fileOpenerService)
        {
            _windowService = windowService;
            _fileOpenerService = fileOpenerService;
        }

        [RelayCommand]
        public void Login()
        {
            // Проверка введены ли данные в окна. Работает, но пока убрал 
            //if (IsCompleted())
                OnLoginSuccess();
        }

        [RelayCommand]
        public void OpenFile()
        {
            _fileOpenerService.OpenFile();
        }

        private void OnLoginSuccess()
        {
            var mainWindow = new Core.Views.MainWindow
            {
                DataContext = App.Services.GetRequiredService<MainWindowViewModel>()
            };

            if (mainWindow.DataContext is MainWindowViewModel a)
            {
                a.MainObservation = ObservationElement;
            }

            mainWindow.Show();

            _windowService.CloseCurrentWindow(mainWindow);
        }

        private bool IsCompleted()
        {
            return ObservationElement.City != "" && ObservationElement.RespondentId != null && ObservationElement.InspectorName != null;
        }
    }
}
