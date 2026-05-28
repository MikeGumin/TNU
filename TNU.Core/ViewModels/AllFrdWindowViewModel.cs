using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TNU.Core.Models;
using TNU.Core.Repository;
using TNU.Core.Services;
using TNU.Core.Services.CloseWindow;
using TNU.Core.Services.FileOpener;
using TNU.Core.Services.FilterLogic;
using TNU.Core.ViewModels.MainWindow;
using TNU.Core.Views;

namespace TNU.Core.ViewModels;

public partial class AllFrdWindowViewModel : ViewModelBase
{
    
    /// <summary>
    /// Поля для фильтра записей из списка ФРД
    /// </summary>
    /// 
    public Avalonia.Controls.ComboBoxItem SelectedField { get; set; }
    public string SelectedOperator { get; set; }
    public string Value { get; set; }
    
    private readonly IWindowService _windowService;
    private readonly IFileOpenerService _fileOpenerService;
    private readonly IFilterLogicService _filterLogicService;

    private ObservableCollection<string> _listOperators = ["=", "!=", ">", "<", ">=", "<="];

    public ObservableCollection<string> ListOperators
    {
        get => _listOperators;
        set
        {
            _listOperators = value;
            OnPropertyChanged();
        }
    }
    
    public IRelayCommand FilterFrdListCommand { get; }


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

    public AllFrdWindowViewModel(
        IFileOpenerService fileOpenerService,
        IFilterLogicService filterLogicService)
    {
        _fileOpenerService = fileOpenerService;
        _filterLogicService = filterLogicService;
        _windowService = new WindowService();
        FilterFrdListCommand = new RelayCommand(ExecuteFilter);
    }

    [RelayCommand]
    private void OpenFrdFile(FrdModel frd)
    {
        _fileOpenerService.OpenFrdFile(frd.FileName);
    }
    
    private void ExecuteFilter()
    {
        string fieldText = SelectedField.Content?.ToString() ?? string.Empty;
        
        var filteredFrd = new ObservableCollection<FrdModel>(
            _filterLogicService.FilterAllFrdList(fieldText, SelectedOperator, Value)
            );
        
        FrdRepository.FinishedFrd.Clear();

        var frdId = 1;
        
        foreach (var item in filteredFrd)
        {
            FrdRepository.FinishedFrd.Add(new FrdModel()
            {
                Id = frdId++,
                FileName = item.FileName,
                CreatedAt =  item.CreatedAt,
                Author =  item.Author,
                Enterprise= item.Enterprise,
            });
        }
    }

    [RelayCommand]
    private void CleaningFrdFilter()
    {
        _filterLogicService.CleaningAllFrdList();
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