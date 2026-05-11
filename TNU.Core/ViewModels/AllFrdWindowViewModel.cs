using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TNU.Core.Models;
using TNU.Core.Services;
using TNU.Core.Services.CloseWindow;
using TNU.Core.Services.FileOpener;
using TNU.Core.ViewModels.MainWindow;

namespace TNU.Core.ViewModels;

public partial class AllFrdWindowViewModel : ViewModelBase
{
    private readonly IWindowService _windowService;
    private readonly IFileOpenerService _fileOpenerService;

    public ViewModelBase ViewModel { get; set; }

    public Observation ObservationElement { get; set; } = new Observation();
    private Observation _mainObservation;
    public Observation MainObservation
    {
        get => _mainObservation;
        set
        {
            if (_mainObservation == null)
            {
                _mainObservation = value;
                OnPropertyChanged();
                //_finishedEntryService.FinishedEntries = value.FinishedEntries;
            }
        }
    }

    public AllFrdWindowViewModel(IFileOpenerService fileOpenerService)
    {
        _fileOpenerService = fileOpenerService;
        _windowService = new WindowService();
    }

    [RelayCommand]
    private void OpenFrdFile(FrdModel frd)
    {
        _fileOpenerService.OpenFrdFile(frd.FileName);
    }

    /// <summary>
    /// Метод закрытия списка ФРД и возвращения к окну фрд 
    /// </summary>
    [RelayCommand]
    private void CloseAllFrdWindow()
    {

        if (ViewModel is LoginWindowViewModel)
        {
            var mainWindow = new Core.Views.LoginWindow
            {
                DataContext = App.Services.GetRequiredService<LoginWindowViewModel>()
            };

            mainWindow.Show();

            _windowService.CloseCurrentWindow(mainWindow);
        }
        else
        {
            var mainWindow = new Core.Views.MainWindow
            {
                DataContext = new MainWindowViewModel(ObservationElement)
            };

            mainWindow.Show();

            _windowService.CloseCurrentWindow(mainWindow);
        }
    }



    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}