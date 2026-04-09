using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace TNU.Core;

/// <summary>
/// Хелпер для вывода сообщений об ошибке
/// </summary>
public class ErrorMessageHelper
{
    /// <summary>
    /// Выводит сообщение об ошибке на экран 
    /// </summary>
    /// <param name="errorField">Место, где произошла ошибка</param>
    /// <param name="errorMessage">Текст ошибки</param>
    /// <param name="mainWindow">Окно приложения, поверх которого всплывает уведомление</param>
    public async Task ShowErrorMessage(
        string errorField,
        string? errorMessage,
        Window mainWindow)
    {
        if (errorMessage is not null)
        {
            var closeButton = new Button
            {
                Content = "Понятно",
                HorizontalAlignment = HorizontalAlignment.Right,
                Padding = new Thickness(20, 8),
                Background = new SolidColorBrush(Color.Parse("#FF6B6B")),
                Foreground = Brushes.White,
                CornerRadius = new CornerRadius(6)
            };
            
            var dialog = new Window
            {
                Title = "Ошибка",
                Width = 440,
                Height = 280,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                Background = new SolidColorBrush(Color.Parse("#1E1E2E")),
                Content = new Border
                {
                    Padding = new Thickness(24),
                    Child = new StackPanel
                    {
                        Spacing = 16,
                        Children =
                        {
                            new TextBlock { Text = $"⚠ {errorField}", FontSize = 16 },
                            new TextBlock { Text = errorMessage, TextWrapping = TextWrapping.Wrap },
                            closeButton
                        }
                    }
                }
            };
            
            closeButton.Click += (s, e) => dialog.Close();

            await dialog.ShowDialog(mainWindow);
        }
    }
}