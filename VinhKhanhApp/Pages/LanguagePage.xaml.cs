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

    async void ChangeLanguage(string lang)
    {
        LocalizationService.CurrentLanguage = lang;

        
        await Navigation.PushAsync(new HomePage());
    }

    void OnVN(object sender, EventArgs e) => ChangeLanguage("vi");
    void OnEN(object sender, EventArgs e) => ChangeLanguage("en");
    void OnDE(object sender, EventArgs e) => ChangeLanguage("de");
    void OnJP(object sender, EventArgs e) => ChangeLanguage("ja");
    void OnZH(object sender, EventArgs e) => ChangeLanguage("zh");
}