using System.Runtime.Versioning;
using VinhKhanhApp.Models;
using VinhKhanhApp.Services;
using Microsoft.Maui.Devices.Sensors;

namespace VinhKhanhApp.Pages;

[SupportedOSPlatform("android")]
[SupportedOSPlatform("ios")]
public partial class HomePage : ContentPage
{
    List<FoodPlace> places = new();
    LocationService locationService = new();
    FoodService foodService = new();

    HashSet<string> spokenPlaces = new();

    public HomePage()
    {
        InitializeComponent();

        TitleLabel.Text = LocalizationService.Translate("title");
        PauseBtn.Text = LocalizationService.Translate("pause");

        locationService.OnLocationChanged += OnLocationChanged;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            places = await foodService.GetFoods();
            FoodList.ItemsSource = places;

            var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
            {
                await locationService.StartListening();
            }
            else
            {
                await DisplayAlertAsync("Error", "Không có quyền GPS", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }

    void OnLocationChanged(Location loc)
    {
        if (loc == null) return;

        foreach (var p in places)
        {
            var distance = Location.CalculateDistance(
                loc,
                new Location(p.Latitude, p.Longitude),
                DistanceUnits.Kilometers);

            if (distance < 0.1 && !spokenPlaces.Contains(p.Name))
            {
                spokenPlaces.Add(p.Name);

                MainThread.BeginInvokeOnMainThread(async () =>
                {
                    await TTSService.Speak(p.Description);
                });
            }
        }
    }

    async void OnPlayClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var place = btn?.CommandParameter as FoodPlace;

        if (place == null) return;

        await TTSService.Speak(place.Description);
    }

    async void OnDirectionClicked(object sender, EventArgs e)
    {
        var btn = sender as Button;
        var place = btn?.CommandParameter as FoodPlace;

        if (place == null) return;

        await Launcher.OpenAsync(
            $"https://www.google.com/maps/dir/?api=1&destination={place.Latitude},{place.Longitude}");
    }

    void OnPause(object sender, EventArgs e)
    {
        locationService.Stop();
        TTSService.Stop();
    }

    async void OnSelected(object sender, SelectionChangedEventArgs e)
    {
        var place = e.CurrentSelection.FirstOrDefault() as FoodPlace;

        if (place == null) return;

        await Navigation.PushAsync(new DetailPage(place));
    }
}