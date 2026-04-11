using System.Runtime.Versioning;
using VinhKhanhApp.Services;

namespace VinhKhanhApp.Pages;

[SupportedOSPlatform("android")]
[SupportedOSPlatform("ios")]
public partial class LanguagePage : ContentPage
{
    public LanguagePage()
    {
        InitializeComponent();
    }

    void OnVN(object sender, EventArgs e)
    {
        LocalizationService.CurrentLanguage = "vi";

        if (Application.Current?.Windows.Count > 0)
        {
            Application.Current.Windows[0].Page =
                new NavigationPage(new HomePage());
        }
    }

    void OnEN(object sender, EventArgs e)
    {
        LocalizationService.CurrentLanguage = "en";

        if (Application.Current?.Windows.Count > 0)
        {
            Application.Current.Windows[0].Page =
                new NavigationPage(new HomePage());
        }
    }
}