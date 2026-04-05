using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using TNU.Core.Services.EntryExport;
using TNU.Core.Services.FileDialog;
using TNU.Core.Services.FinishedEntry;
using LoginWindow = TNU.Core.Views.LoginWindow;
using LoginWindowViewModel = TNU.Core.ViewModels.LoginWindowViewModel;
using MainWindowViewModel = TNU.Core.ViewModels.MainWindowViewModel;

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
            collection.AddScoped<IEntryExportService, EntryExportService>();
            collection.AddSingleton<MainWindowViewModel>();
            collection.AddSingleton<LoginWindowViewModel>();
            collection.AddSingleton<ErrorMessageHelper>();
            collection.AddSingleton<OperationResult>();
            collection.AddScoped<IFinishedEntryService, FinishedEntryService>();

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