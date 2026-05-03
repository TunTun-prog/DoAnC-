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

    async Task ChangeLanguage(string lang)
    {
        try
        {
            LocalizationService.CurrentLanguage = lang;

            // Use Application.Current.MainPage to ensure a navigation page is set
            Application.Current.MainPage = new NavigationPage(new HomePage());
        }
        catch (Exception ex)
        {
            // Show a friendly error if navigation fails
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    async void OnVN(object sender, EventArgs e) => await ChangeLanguage("vi");
    async void OnEN(object sender, EventArgs e) => await ChangeLanguage("en");
    async void OnDE(object sender, EventArgs e) => await ChangeLanguage("de");
    async void OnJP(object sender, EventArgs e) => await ChangeLanguage("ja");
    async void OnZH(object sender, EventArgs e) => await ChangeLanguage("zh");
}