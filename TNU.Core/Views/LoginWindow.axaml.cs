using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.DependencyInjection;
using LoginWindowViewModel = TNU.Core.ViewModels.LoginWindowViewModel;
using MainWindowViewModel = TNU.Core.ViewModels.MainWindowViewModel;

namespace TNU.Core.Views;

public partial class LoginWindow : Window
{
    public LoginWindow()
    {
        InitializeComponent();
    }
    
    /// <summary>
    /// Вызывается Avalonia автоматически при каждом изменении DataContext.
    /// Используем override вместо события DataContextChanged,
    /// чтобы гарантировать подписку после того как DataContext уже установлен.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);

        if (DataContext is LoginWindowViewModel vm)
        {
            // защита от дублирования окно 
            vm.LoginSuccess -= OnLoginSuccess;
            vm.LoginSuccess += OnLoginSuccess;
        }
    }

    /// <summary>
    /// Вызывается когда LoginWindowViewModel сигнализирует об успешном входе.
    /// Создаём главное окно, передаём ему ViewModel из DI,
    /// назначаем его главным окном приложения и закрываем окно логина.
    /// </summary>
    private void OnLoginSuccess()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainWindow = new Core.Views.MainWindow
            {
                DataContext = App.Services.GetRequiredService<MainWindowViewModel>()
            };
            mainWindow.Show();
            desktop.MainWindow = mainWindow;
            Close();
        }
    }
}