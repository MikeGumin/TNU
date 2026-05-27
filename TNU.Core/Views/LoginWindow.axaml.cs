using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace TNU.Core.Views;

public partial class LoginWindow : Window
{
    private bool _isMenuOpen = false;

    private const string BurgerGeometry = "M0,2 H22 M0,9 H22 M0,16 H22";
    private const string CloseGeometry  = "M2,2 L20,20 M20,2 L2,20";
    
    public LoginWindow()
    {
        InitializeComponent();
    }
    

    private void HamburgerButton_Click(object? sender, RoutedEventArgs e)
    {
        _isMenuOpen = !_isMenuOpen;
        mySplitView.IsPaneOpen = _isMenuOpen;
        BurgerIcon.Data = Geometry.Parse(_isMenuOpen ? CloseGeometry : BurgerGeometry);
    }
}