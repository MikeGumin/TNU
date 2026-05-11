using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace TNU.Core.Services.CloseWindow
{
    public interface IWindowService
    {
        public IClassicDesktopStyleApplicationLifetime CurrentWindow { get; set; }
        void CloseCurrentWindow(Window window);
    }
}
