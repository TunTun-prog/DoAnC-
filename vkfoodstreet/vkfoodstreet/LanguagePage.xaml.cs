namespace vkfoodstreet;

public partial class LanguagePage : ContentPage
{
    public LanguagePage()
    {
        InitializeComponent();
    }

    private async void OnVietnameseClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage("vi"));
    }

    private async void OnEnglishClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage("en"));
    }
}