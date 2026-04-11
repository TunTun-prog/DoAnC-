using Microsoft.Maui.Media;

namespace VinhKhanhApp.Services;

public static class TTSService
{
    public static async Task Speak(string text)
    {
        if (DeviceInfo.Platform == DevicePlatform.Android ||
            DeviceInfo.Platform == DevicePlatform.iOS)
        {
            await TextToSpeech.Default.SpeakAsync(text);
        }
    }
}