using System;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Plugin.Maui.Geolocation;

namespace GpsAttendanceApp.Views;

public partial class GpsLoginPage : ContentPage
{
    public GpsLoginPage()
    {
        InitializeComponent();
    }

    private async void OnGpsLoginClicked(object sender, EventArgs e)
    {
        StatusLabel.IsVisible = false;

        try
        {
            var location = await Geolocation.Default.GetLocationAsync();

            if (location != null)
            {
                // TODO: Send location to backend for GPS login verification
                // For now, navigate to main attendance page
                await Navigation.PushAsync(new AttendancePage());
            }
            else
            {
                StatusLabel.Text = "Unable to get location. Please try again.";
                StatusLabel.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Error: {ex.Message}";
            StatusLabel.IsVisible = true;
        }
    }
}
