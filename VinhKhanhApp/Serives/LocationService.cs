using Microsoft.Maui.Devices.Sensors;

namespace VinhKhanhApp.Services;

public class LocationService
{
    public event Action<Location>? OnLocationChanged;

    CancellationTokenSource? cts;

    public async Task StartListening()
    {
        if (cts != null) return;

        cts = new CancellationTokenSource();

        try
        {
            while (!cts.IsCancellationRequested)
            {
                try
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
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }

                await Task.Delay(3000, cts.Token);
            }
        }
        catch (TaskCanceledException)
        {
           
        }
    }

    public void Stop()
    {
        cts?.Cancel();
        cts = null;
    }
}