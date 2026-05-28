using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TNU.Core.Services;
using TNU.Core.Services.CloseWindow;
using TNU.Core.Services.EntryExport;
using TNU.Core.Services.FileDialog;
using TNU.Core.Services.FileOpener;
using TNU.Core.Services.FilterLogic;
using TNU.Core.Services.FinishedEntry;
using TNU.Core.ViewModels;
using TNU.Core.Views.DialogViews;
using LoginWindow = TNU.Core.Views.LoginWindow;
using LoginWindowViewModel = TNU.Core.ViewModels.LoginWindowViewModel;
using MainPageViewModel = TNU.Core.ViewModels.Page.MainPage.MainPageViewModel;

namespace TNU.Core;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; } = null!;

    // Никакого конструктора с параметрами!

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<MainPageViewModel>();
            collection.AddSingleton<ConfirmDialog>();
            collection.AddSingleton<InformationWindow>();
            collection.AddSingleton<LoginWindowViewModel>();
            collection.AddSingleton<AllFrdWindowViewModel>();
            collection.AddSingleton<FrdPageViewModel>();
            collection.AddSingleton<ErrorMessageHelper>();
            collection.AddSingleton<OperationResult>();

            // что-то делает, теперь окана можн озакрыть из ViewModel
            collection.AddSingleton<IWindowService, WindowService>();

            collection.AddScoped<IEntryExportService, EntryExportService>();
            collection.AddScoped<IFileOpenerService, FileOpenerService>();
            collection.AddScoped<IFinishedEntryService, FinishedEntryService>();
            collection.AddScoped<IFilterLogicService, FilterLogicService>();

            // Передаём Func — TopLevel будет получен позже, в момент вызова
            collection.AddSingleton<IFileDialogService>(_ =>
                new FileDialogService(() =>
                {
                    if (Application.Current?.ApplicationLifetime
                        is IClassicDesktopStyleApplicationLifetime d)
                    {
                        return TopLevel.GetTopLevel(d.MainWindow);
                    }
                    return null;
                }));

            Services = collection.BuildServiceProvider();

            desktop.MainWindow = new LoginWindow()
            {
                DataContext = Services.GetRequiredService<LoginWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}