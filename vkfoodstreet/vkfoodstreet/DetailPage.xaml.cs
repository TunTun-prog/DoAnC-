namespace vkfoodstreet;

public partial class DetailPage : ContentPage
{
    public DetailPage(FoodPlace item, string lang)
    {
        InitializeComponent();

        nameLabel.Text = item.Name;
        img1.Source = item.Image1;
        img2.Source = item.Image2;

        descLabel.Text = lang == "vi"
            ? item.DescriptionVI
            : item.DescriptionEN;
    }
}