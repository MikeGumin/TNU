using System.ComponentModel;
using System.Runtime.CompilerServices;
using TNU.Core.Models;
using TNU.Core.Repository;
using TNU.Core.Services;
using TNU.Core.Services.CloseWindow;

namespace TNU.Core.ViewModels;

public class AllFrdWindowViewModel : ViewModelBase
{
    private readonly IWindowService _windowService;
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

    public AllFrdWindowViewModel()
    {
        _windowService = new WindowService();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}