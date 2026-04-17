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
                ["pause"] = "Tạm dừng"
            },

            ["en"] = new Dictionary<string, string>()
            {
                ["title"] = "Vinh Khanh Food Street",
                ["pause"] = "Pause"
            },

            ["de"] = new Dictionary<string, string>()
            {
                ["title"] = "Vinh Khanh Essensstraße",
                ["pause"] = "Pause"
            },

            ["ja"] = new Dictionary<string, string>()
            {
                ["title"] = "ビンカイン屋台通り",
                ["pause"] = "一時停止"
            },

            ["zh"] = new Dictionary<string, string>()
            {
                ["title"] = "永庆美食街",
                ["pause"] = "暂停"
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