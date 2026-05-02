using System.Net;
using System.Text.Json;

namespace VinhKhanhApp.Services;

public class TranslateService
{
    HttpClient client = new HttpClient();

    public async Task<string> Translate(string text, string targetLang)
    {
        try
        {
            if (targetLang == "vi") return text;

            string url =
                $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=vi&tl={targetLang}&dt=t&q={WebUtility.UrlEncode(text)}";

            var response = await client.GetStringAsync(url);

            using var doc = JsonDocument.Parse(response);

            var result = doc.RootElement[0][0][0].GetString();

            return result ?? text;
        }
        catch
        {
            return text;
        }
    }
}