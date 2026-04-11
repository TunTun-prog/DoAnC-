namespace VinhKhanhApp.Services;

public static class LocalizationService
{
    public static string CurrentLanguage = "vi";

    public static string Translate(string key)
    {
        var vi = new Dictionary<string, string>()
        {
            {"title", "Phố ăn vặt Vĩnh Khánh"},
            {"direction", "Chỉ đường"},
            {"pause", "Tạm dừng"}
        };

        var en = new Dictionary<string, string>()
        {
            {"title", "Vinh Khanh Food Street"},
            {"direction", "Directions"},
            {"pause", "Pause"}
        };

        return CurrentLanguage == "vi" ? vi[key] : en[key];
    }
}