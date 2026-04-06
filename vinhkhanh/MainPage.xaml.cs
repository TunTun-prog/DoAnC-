using System.Collections.ObjectModel;

namespace vinhkhanh;

public partial class MainPage : ContentPage
{
    ObservableCollection<FoodPlace> places = new();
    string currentLang;

    CancellationTokenSource? _ttsCts;

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
                Latitude = 10.7625,
                Longitude = 106.7041,
                Description = "Lãng Quán là quán ăn nổi tiếng tại phố ẩm thực Vĩnh Khánh, chuyên phục vụ các món lẩu, nướng và hải sản đa dạng với không gian ngoài trời thoáng mát và giá cả hợp lý."
            }
        };

        listView.ItemsSource = places;
    }

    FoodPlace? GetItem(object sender)
    {
        return (sender as Button)?.CommandParameter as FoodPlace;
    }

    async Task Speak(string text)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            _ttsCts?.Cancel();
            _ttsCts = new CancellationTokenSource();

            var options = new SpeechOptions
            {
                Pitch = 1.0f,
                Volume = 1.0f
            };

            await TextToSpeech.Default.SpeakAsync(text, options, _ttsCts.Token);
        }
        catch
        {
            
        }
    }

    private async void OnPlayClicked(object sender, EventArgs e)
    {
        var item = GetItem(sender);
        if (item == null) return;

        string text = item.Description;

        if (string.IsNullOrWhiteSpace(text))
            text = item.Name;

        await Speak(text);
    }

    private async void OnNavigateClicked(object sender, EventArgs e)
    {
        var item = GetItem(sender);
        if (item == null) return;

        await Navigation.PushAsync(new DetailPage(item, currentLang));
    }
}