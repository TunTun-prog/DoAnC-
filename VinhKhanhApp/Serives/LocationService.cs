using Microsoft.Maui.Devices.Sensors;

namespace VinhKhanhApp.Services;

public class LocationService
{
    public event Action<Location>? OnLocationChanged;

    bool isTracking = false;

    public async Task StartListening()
    {
        if (isTracking) return;

        isTracking = true;

        while (isTracking)
        {
            try
            {
                if (DeviceInfo.Platform == DevicePlatform.Android ||
                    DeviceInfo.Platform == DevicePlatform.iOS)
                {
                    var location = await Geolocation.Default.GetLocationAsync(
                        new GeolocationRequest(GeolocationAccuracy.Medium));

                    if (location != null)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            OnLocationChanged?.Invoke(location);
                        });
                    }
                }
            }
            catch { }

            await Task.Delay(3000);
        }
    }

    public void Stop()
    {
        isTracking = false;
    }
}