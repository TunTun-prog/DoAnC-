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
    TranslateService translateService = new();

    HashSet<string> spokenPlaces = new();
    bool isLoaded = false;

    public HomePage()
    {
        InitializeComponent();

        locationService.OnLocationChanged += OnLocationChanged;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!isLoaded)
        {
            isLoaded = true;
            await LoadDataAsync();
        }

        TitleLabel.Text = LocalizationService.Translate("title");
        PauseBtn.Text = LocalizationService.Translate("pause");
    }

    private async Task LoadDataAsync()
    {
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

    async void OnLocationChanged(Location loc)
    {
        if (loc == null || places == null) return;

        foreach (var p in places)
        {
            var distance = Location.CalculateDistance(
                loc,
                new Location(p.Latitude, p.Longitude),
                DistanceUnits.Kilometers);

            if (distance < 0.1 && spokenPlaces.Add(p.Name))
            {
                var translated = await translateService.Translate(
                    p.Description,
                    LocalizationService.CurrentLanguage);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    _ = TTSService.Speak(translated);
                });
            }
        }
    }

    async void OnPlayClicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.CommandParameter is not FoodPlace place)
            return;

        var translated = await translateService.Translate(
            place.Description,
            LocalizationService.CurrentLanguage);

        await TTSService.Speak(translated);
    }

    async void OnDirectionClicked(object sender, EventArgs e)
    {
        if ((sender as Button)?.CommandParameter is not FoodPlace place)
            return;

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
        if (e.CurrentSelection.FirstOrDefault() is not FoodPlace place)
            return;

        ((CollectionView)sender).SelectedItem = null;

        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            await Navigation.PushAsync(new DetailPage(place));
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        locationService.OnLocationChanged -= OnLocationChanged;
        locationService.Stop();
        TTSService.Stop();
    }
}