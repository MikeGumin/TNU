using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using TNU.Core.ViewModels;

namespace TNU.Core;

/// <summary>
/// Given a view model, returns the corresponding view if possible.
/// </summary>
[RequiresUnreferencedCode(
    "Default implementation of ViewLocator involves reflection which may be trimmed away.",
    Url = "https://docs.avaloniaui.net/docs/concepts/view-locator")]

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return new TextBlock { Text = "Data is null" };

        var vmFullName = param.GetType().FullName!;

        // === ИСПРАВЛЕНИЕ ===
        // Заменяем ViewModel на View и исправляем namespace
        var viewFullName = vmFullName
            .Replace("ViewModel", "View", StringComparison.Ordinal)
            .Replace(".ViewModels.", ".");   // Убираем .ViewModels. → .

        var viewType = Type.GetType(viewFullName);

        if (viewType != null)
        {
            var control = (Control)Activator.CreateInstance(viewType)!;

            if (control.DataContext == null)
                control.DataContext = param;

            return control;
        }

        // Подробная ошибка для отладки
        return new TextBlock
        {
            Text = $"View not found!\n\n" +
                   $"ViewModel: {vmFullName}\n" +
                   $"Искал View: {viewFullName}",
            Foreground = Avalonia.Media.Brushes.Red,
            Margin = new Avalonia.Thickness(20)
        };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}