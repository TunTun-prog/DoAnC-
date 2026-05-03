using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Versioning;
using System.Text.Json;
using Microsoft.Maui.Storage;
using VinhKhanhApp.Models;

namespace VinhKhanhApp.Pages;

[SupportedOSPlatform("android")]
[SupportedOSPlatform("ios")]
public partial class MapPage : ContentPage
{
    public MapPage(FoodPlace place)
    {
        InitializeComponent();

        _ = LoadMapAsync(place);
    }

    async Task LoadMapAsync(FoodPlace place)
    {
        try
        {
            // Read map.html from app package (Resources/Raw)
            using var stream = await FileSystem.OpenAppPackageFileAsync("map.html");
            using var reader = new StreamReader(stream);
            var html = await reader.ReadToEndAsync();

            // Inject numeric values using invariant culture and safely serialize label
            var lat = place.Latitude.ToString(CultureInfo.InvariantCulture);
            var lng = place.Longitude.ToString(CultureInfo.InvariantCulture);
            var labelJson = JsonSerializer.Serialize(place.Name);

            html = html.Replace("__LAT__", lat)
                       .Replace("__LNG__", lng)
                       .Replace("__LABEL__", labelJson);

            webView.Source = new HtmlWebViewSource { Html = html };
        }
        catch (Exception ex)
        {
            await DisplayAlert("Map error", ex.Message, "OK");
        }
    }
}
