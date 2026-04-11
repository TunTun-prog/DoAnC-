using System.Runtime.Versioning;
using VinhKhanhApp.Models;
using VinhKhanhApp.Services;
using Microsoft.Maui.Devices.Sensors;

namespace VinhKhanhApp.Pages;

[SupportedOSPlatform("android")]
[SupportedOSPlatform("ios")]
public partial class HomePage : ContentPage
{
    List<FoodPlace> places;
    LocationService locationService = new LocationService();
    bool hasSpoken = false;

    public HomePage()
    {
        InitializeComponent();

        TitleLabel.Text = LocalizationService.Translate("title");
        PauseBtn.Text = LocalizationService.Translate("pause");

        places = new List<FoodPlace>()
        {
            new FoodPlace{
                Name="Ốc Oanh",
                Description="Quán ốc nổi tiếng tại Vĩnh Khánh",
                Latitude=10.759,
                Longitude=106.705,
                Rating=4.5
            },
            new FoodPlace{
                Name="Ốc Đào",
                Description="Quán ốc đông khách, giá hợp lý",
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

    void OnLocationChanged(Location loc)
    {
        if (hasSpoken) return;

        foreach (var p in places)
        {
            var distance = Location.CalculateDistance(
                loc,
                new Location(p.Latitude, p.Longitude),
                DistanceUnits.Kilometers);

            if (distance < 0.1)
            {
                hasSpoken = true;
                _ = TTSService.Speak(p.Description);
            }
        }
    }

    void OnPause(object sender, EventArgs e)
    {
        locationService.Stop();
    }

    async void OnSelected(object sender, SelectionChangedEventArgs e)
    {
        var place = e.CurrentSelection.FirstOrDefault() as FoodPlace;

        if (place != null)
        {
            await Navigation.PushAsync(new DetailPage(place));
        }
    }
}