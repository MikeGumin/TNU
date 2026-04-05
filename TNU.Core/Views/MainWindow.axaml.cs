using Avalonia.Controls;
using MainWindowViewModel = TNU.Core.ViewModels.MainWindowViewModel;

namespace TNU.Core.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Нужно для передачи в контекст родительского окна
        this.Loaded += (sender, e) =>
        {
            if (DataContext is MainWindowViewModel vm)
            {
                vm.MainWindow = this;
            }
        };
    }
}