using VinhKhanhApp.Pages;

namespace VinhKhanhApp;

public partial class App : Microsoft.Maui.Controls.Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new NavigationPage(new LanguagePage()));
    }
}