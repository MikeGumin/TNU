using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using TNU.Core.Services.CloseWindow;

namespace TNU.Core.Services
{
    
    public class WindowService : IWindowService
    {
        public void CloseCurrentWindow()
        {
            if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime)
            {
                lifetime.MainWindow?.Close();
            }
        }

    }
}
