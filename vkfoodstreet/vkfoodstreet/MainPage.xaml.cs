using System.Collections.ObjectModel;
using Microsoft.Maui.Media;
using System.Linq;

namespace vkfoodstreet;

public partial class MainPage : ContentPage
{
    ObservableCollection<FoodPlace> places;
    string currentLang;
    CancellationTokenSource cts;

    public MainPage(string lang)
    {
        InitializeComponent();
        currentLang = lang;

        places = new ObservableCollection<FoodPlace>
        {
            new FoodPlace
            {
                Name = "Lãng Quán",
                Address = "122 Vĩnh Khánh, Q4",
                Image1 = "lang1.jpg",
                Image2 = "lang2.jpg",
                Rating = 4.3,
                DescriptionVI = "Lãng Quán nổi tiếng với món nướng và không gian đẹp.",
                DescriptionEN = "Lang Quan is famous for grilled food and a beautiful atmosphere."
            },
            new FoodPlace
            {
                Name = "Ớt xiêm quán",
                Address = "589 Vĩnh Khánh, Q4",
                Image1 = "ot1.jpg",
                Image2 = "ot2.jpg",
                Rating = 4.1,
                DescriptionVI = "Quán chuyên món cay và hải sản.",
                DescriptionEN = "Spicy seafood restaurant."
            },
            new FoodPlace
            {
                Name = "Sushi Ko",
                Address = "122 Vĩnh Khánh, Q4",
                Image1 = "sushi1.jpg",
                Image2 = "sushi2.jpg",
                Rating = 4.2,
                DescriptionVI = "Quán sushi tươi ngon.",
                DescriptionEN = "Fresh sushi."
            }
        };

        listView.ItemsSource = places;
    }

    private async void OnPlayClicked(object sender, EventArgs e)
    {
        try
        {
            var btn = sender as Button;
            var item = btn.BindingContext as FoodPlace;

            if (btn.Text == "▶")
            {
                btn.Text = "⏸";

                string text = currentLang == "vi"
                    ? item.DescriptionVI
                    : item.DescriptionEN;

                
                await Task.Delay(300);

                await TextToSpeech.Default.SpeakAsync(text);
            }
            else
            {
                btn.Text = "▶";
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Lỗi", ex.Message, "OK");
        }
    }

    private async void OnItemTapped(object sender, EventArgs e)
    {
        var frame = sender as Frame;
        var item = frame.BindingContext as FoodPlace;

        await Navigation.PushAsync(new DetailPage(item, currentLang));
    }
}