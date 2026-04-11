using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Devices.Sensors;
using Microsoft.Maui.Maps;
using System.Runtime.Versioning;
using VinhKhanhApp.Models;

namespace VinhKhanhApp.Pages;

[SupportedOSPlatform("android")]
[SupportedOSPlatform("ios")]
public partial class MapPage : ContentPage
{
    public MapPage(FoodPlace place)
    {
        InitializeComponent();

        var location = new Location(place.Latitude, place.Longitude);

        var pin = new Pin
        {
            Label = place.Name,
            Location = location
        };

        map.Pins.Add(pin);

        map.MoveToRegion(
            MapSpan.FromCenterAndRadius(
                location,
                Distance.FromMeters(500)
            )
        );
    }
}