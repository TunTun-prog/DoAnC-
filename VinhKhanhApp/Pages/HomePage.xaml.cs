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

    HashSet<string> spokenPlaces = new();

    public HomePage()
    {
        InitializeComponent();

        TitleLabel.Text = LocalizationService.Translate("title");
        PauseBtn.Text = LocalizationService.Translate("pause");

        places = new List<FoodPlace>()
        {
            new FoodPlace{
                Name="Ốc Oanh",
                Description=LocalizationService.Translate("oc_oanh"),
                Latitude=10.759,
                Longitude=106.705,
                Rating=4.5
            },
            new FoodPlace{
                Name="Ốc Đào",
                Description=LocalizationService.Translate("oc_dao"),
                Latitude=10.760,
                Longitude=106.706,
                Rating=4.6
            }
        };

        FoodList.ItemsSource = places;

        locationService.OnLocationChanged += OnLocationChanged;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
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

    void OnPause(object sender, EventArgs e)
    {
        locationService.Stop();
        TTSService.Stop(); 
    }

    async void OnSelected(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            var place = e.CurrentSelection.FirstOrDefault() as FoodPlace;

            if (place == null) return;

            await Navigation.PushAsync(new DetailPage(place));
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }
}