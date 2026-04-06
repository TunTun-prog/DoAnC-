using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace vinhkhanh;

public partial class DetailPage : ContentPage
{
    FoodPlace currentItem;

    public DetailPage(FoodPlace item, string lang)
    {
        InitializeComponent();

        currentItem = item;

        nameLabel.Text = item.Name;
        img1.Source = item.Image1;
        img2.Source = item.Image2;
        descLabel.Text = item.Description;

        SetupMap(item);
    }

    void SetupMap(FoodPlace item)
    {
        var position = new Location(item.Latitude, item.Longitude);

        var pin = new Pin
        {
            Label = item.Name,
            Location = position,
            Type = PinType.Place
        };

        MyMap.Pins.Add(pin);

        MyMap.MoveToRegion(
            MapSpan.FromCenterAndRadius(position, Distance.FromMeters(500))
        );
    }

    void LocateBtn_Clicked(object sender, EventArgs e)
    {
        var position = new Location(currentItem.Latitude, currentItem.Longitude);

        MyMap.MoveToRegion(
            MapSpan.FromCenterAndRadius(position, Distance.FromMeters(300))
        );
    }
}