using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using TNU.Core.Services.CloseWindow;

namespace TNU.Core.Services
{
    
    public class WindowService : IWindowService
    {
        public IClassicDesktopStyleApplicationLifetime CurrentWindow { get; set; } = (IClassicDesktopStyleApplicationLifetime)Application.Current?.ApplicationLifetime;

        public void CloseCurrentWindow(Window window)
        {
                CurrentWindow.MainWindow?.Close();
                CurrentWindow.MainWindow = window;
        }

    }
}
