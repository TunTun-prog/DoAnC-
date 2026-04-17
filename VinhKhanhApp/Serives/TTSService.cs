using Microsoft.Maui.Media;

namespace VinhKhanhApp.Services;

public static class TTSService
{
    static CancellationTokenSource? cts;

    public static async Task Speak(string text)
    {
        try
        {
            
            cts?.Cancel();
            cts = new CancellationTokenSource();

            var locales = await TextToSpeech.Default.GetLocalesAsync();

            Locale? selectedLocale = null;

            string lang = LocalizationService.CurrentLanguage;

            if (lang == "vi")
                selectedLocale = locales.FirstOrDefault(l => l.Language.StartsWith("vi"));
            else if (lang == "en")
                selectedLocale = locales.FirstOrDefault(l => l.Language.StartsWith("en"));
            else if (lang == "de")
                selectedLocale = locales.FirstOrDefault(l => l.Language.StartsWith("de"));
            else if (lang == "ja")
                selectedLocale = locales.FirstOrDefault(l => l.Language.StartsWith("ja"));
            else if (lang == "zh")
                selectedLocale = locales.FirstOrDefault(l => l.Language.StartsWith("zh"));

            var options = new SpeechOptions
            {
                Locale = selectedLocale, 
                Pitch = 1.0f,
                Volume = 1.0f
            };

            await TextToSpeech.Default.SpeakAsync(text, options, cts.Token);
        }
        catch (TaskCanceledException)
        {
          
        }
        catch (Exception)
        {
            
        }
    }

    public static void Stop()
    {
        try
        {
            cts?.Cancel();
        }
        catch { }
    }
}