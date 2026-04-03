using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TNU.Services.EntryExport;
using TNU.Services.FileDialog;
using TNU.Services.FinishedEntry;
using TNU.ViewModels;
using TNU.Views;

namespace TNU;

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

            desktop.MainWindow = new Views.MainWindow
            {
                DataContext = Services.GetRequiredService<MainWindowViewModel>()
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