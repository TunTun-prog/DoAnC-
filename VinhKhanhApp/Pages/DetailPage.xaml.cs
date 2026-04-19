using VinhKhanhApp.Models;
using VinhKhanhApp.Services;

namespace VinhKhanhApp.Pages;

public partial class DetailPage : ContentPage
{
    FoodPlace place;
    TranslateService translateService = new(); // 🔥 thêm

    public DetailPage(FoodPlace p)
    {
        InitializeComponent();

        place = p;

        NameLabel.Text = p.Name;
        DescLabel.Text = p.Description;
        AddressLabel.Text = p.Address;
        RatingLabel.Text = "Rating: " + p.Rating;

        DirectionBtn.Text = LocalizationService.Translate("direction");
    }

    async void OnPlay(object sender, EventArgs e)
    {
        var translated = await translateService.Translate(
            place.Description,
            LocalizationService.CurrentLanguage);

        await TTSService.Speak(translated);
    }

    async void OnDirection(object sender, EventArgs e)
    {
        await Launcher.OpenAsync(
            $"https://www.google.com/maps/dir/?api=1&destination={place.Latitude},{place.Longitude}");
    }
}