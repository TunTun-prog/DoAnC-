namespace VinhKhanhApp.Services;

public class TranslateService
{
    public async Task<string> Translate(string text, string lang)
    {
        await Task.Delay(10);
        return text;
    }
}