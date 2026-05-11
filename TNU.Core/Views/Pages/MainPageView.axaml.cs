using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using TNU.Core.Models;
using TNU.Core.Repository;
using TNU.Core.Services.CsvFile;
using TNU.Core.ViewModels;

namespace TNU.Core;

public partial class MainPageView : UserControl
{
    public MainPageView()
    {
        InitializeComponent();

        //DataContext = App.Services.GetRequiredService<MainPageViewModel>();
    }
}