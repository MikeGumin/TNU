using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using TNU.Core.Models;
using TNU.Core.Services;
using TNU.Core.Services.CloseWindow;

namespace TNU.Core.ViewModels;

public partial class MainPageViewModel
{
    public Observation ObservationElement { get; set; } = new Observation();

    public MainPageViewModel()
    {
        _windowService = new WindowService();
    }

    /// <summary>
    /// Метод для перехода к окну сохраненных записей ФРД
    /// </summary>
    [RelayCommand]
    private void OpenFrdWindow()
    {
        var frdWindow = new Views.FrdWindow()
        {
            DataContext = App.Services.GetRequiredService<FrdPageViewModel>()
        };

        if (frdWindow.DataContext is FrdPageViewModel a)
        {
            a.MainObservation = ObservationElement;
        }

        frdWindow.Show();

        _windowService.CloseCurrentWindow(frdWindow);
    }
}