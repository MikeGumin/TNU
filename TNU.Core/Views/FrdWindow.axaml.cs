using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Input;
using TNU.Core.Models;
using TNU.Core.Repository;
using TNU.Core.ViewModels;

namespace TNU.Core.Views;

public partial class FrdWindow : Window
{
    public FrdWindow()
    {
        this.DataContext = this;
        InitializeComponent();
    }
    
    [RelayCommand]
    private void DeleteEntry(JobEntry entry)
    {
        // todo: сделать окно проверки и переделать логику под viewModel
        FinishedEntriesRepository.FinishedEntries.Remove(entry);
    }
}