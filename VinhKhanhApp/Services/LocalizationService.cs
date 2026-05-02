namespace VinhKhanhApp.Services;

public static class LocalizationService
{
    public static string CurrentLanguage = "vi";

    static Dictionary<string, Dictionary<string, string>> data =
        new Dictionary<string, Dictionary<string, string>>()
        {
            ["vi"] = new Dictionary<string, string>()
            {
                ["title"] = "Phố ăn vặt Vĩnh Khánh",
                ["pause"] = "Tạm dừng",
                ["direction"] = "Chỉ đường",
                ["play"] = "Phát"
            },

            ["en"] = new Dictionary<string, string>()
            {
                ["title"] = "Vinh Khanh Food Street",
                ["pause"] = "Pause",
                ["direction"] = "Direction",
                ["play"] = "Play"
            },

            ["de"] = new Dictionary<string, string>()
            {
                ["title"] = "Vinh Khanh Essensstraße",
                ["pause"] = "Pause",
                ["direction"] = "Richtung",
                ["play"] = "Abspielen"
            },

            ["ja"] = new Dictionary<string, string>()
            {
                ["title"] = "ビンカイン屋台通り",
                ["pause"] = "一時停止",
                ["direction"] = "方向",
                ["play"] = "再生"
            },

            ["zh"] = new Dictionary<string, string>()
            {
                ["title"] = "永庆美食街",
                ["pause"] = "暂停",
                ["direction"] = "导航",
                ["play"] = "播放"
            }
        };

    public static string Translate(string key)
    {
        if (data.ContainsKey(CurrentLanguage) &&
            data[CurrentLanguage].ContainsKey(key))
        {
            return data[CurrentLanguage][key];
        }

        return key;
    }
}