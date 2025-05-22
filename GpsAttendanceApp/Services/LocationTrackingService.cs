using System;
using System.Threading;
using System.Threading.Tasks;
using Plugin.Maui.Geolocation;

namespace GpsAttendanceApp.Services;

public class LocationTrackingService
{
    private CancellationTokenSource? _cts;

    public async Task StartTrackingAsync()
    {
        _cts = new CancellationTokenSource();

        try
        {
            while (!_cts.IsCancellationRequested)
            {
                var location = await Geolocation.Default.GetLocationAsync();

                if (location != null)
                {
                    // TODO: Send location to backend for real-time tracking
                }

                await Task.Delay(TimeSpan.FromMinutes(1), _cts.Token);
            }
        }
        catch (TaskCanceledException)
        {
            // Tracking stopped
        }
        catch (Exception ex)
        {
            // Handle exceptions
        }
    }

    public void StopTracking()
    {
        _cts?.Cancel();
    }
}
