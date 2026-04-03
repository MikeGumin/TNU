using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using TNU.Views;

namespace TNU;

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
            var dialog = new Window
            {
                Title = errorField,
                Content = new TextBlock { Text = errorMessage },
                Width = 300, Height = 150,
                CanResize = false,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            await dialog.ShowDialog(mainWindow);
        }
    }
}