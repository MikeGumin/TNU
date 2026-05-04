using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.Input;
using TNU.Core.Models;
using TNU.Core.Repository;
using TNU.Core.ViewModels;
using TNU.Core.Views.DialogViews;

namespace TNU.Core.Views;

public partial class FrdWindow : Window
{
    public FrdWindow()
    {
        this.DataContext = this;
        InitializeComponent();
    }
    
    /// <summary>
    /// Метод для удаления сохраненной записи
    /// </summary>
    /// <param name="entry"></param>
    /// 

}