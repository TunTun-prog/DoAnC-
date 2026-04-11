using System.Xml;
using VinhKhanhApp.Models;
using VinhKhanhApp.Services;

namespace VinhKhanhApp.Pages;

public partial class DetailPage : ContentPage
{
    FoodPlace place;

    public DetailPage(FoodPlace p)
    {
        InitializeComponent();

        place = p;

        NameLabel.Text = p.Name;
        DescLabel.Text = p.Description;
        RatingLabel.Text = "Rating: " + p.Rating;

        DirectionBtn.Text = LocalizationService.Translate("direction");
    }

    async void OnDirection(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MapPage(place));
    }
}