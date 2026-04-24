using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using TNU.Core.Models;
using TNU.Core.Services;
using TNU.Core.Services.CloseWindow;

namespace TNU.Core.ViewModels.MainWindow;

public partial class MainWindowViewModel
{
    private readonly IWindowService _windowService;
    public Observation ObservationElement { get; set; }

    public MainWindowViewModel()
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
            DataContext = App.Services.GetRequiredService<FrdWindowViewModel>()
        };

        if (frdWindow.DataContext is FrdWindowViewModel a)
        {
            a.MainObservation = ObservationElement;
        }

        frdWindow.Show();

        _windowService.CloseCurrentWindow();
    }
}