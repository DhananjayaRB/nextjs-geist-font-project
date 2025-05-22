using System;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Plugin.Maui.Geolocation;

namespace GpsAttendanceApp.Views;

public partial class AttendancePage : ContentPage
{
    private bool isCommuteStarted = false;
    private bool isBreakStarted = false;
    private Location? commuteStartLocation;
    private Location? commuteEndLocation;
    private double totalDistanceTravelled = 0;

    public AttendancePage()
    {
        InitializeComponent();
    }

    private async void OnStartCommuteClicked(object sender, EventArgs e)
    {
        try
        {
            commuteStartLocation = await Geolocation.Default.GetLocationAsync();
            if (commuteStartLocation != null)
            {
                isCommuteStarted = true;
                StatusLabel.Text = "Commute started.";
                StatusLabel.TextColor = Colors.Green;
                StatusLabel.IsVisible = true;
            }
            else
            {
                StatusLabel.Text = "Unable to get location.";
                StatusLabel.TextColor = Colors.Red;
                StatusLabel.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Error: {ex.Message}";
            StatusLabel.TextColor = Colors.Red;
            StatusLabel.IsVisible = true;
        }
    }

    private async void OnEndCommuteClicked(object sender, EventArgs e)
    {
        if (!isCommuteStarted)
        {
            StatusLabel.Text = "Commute not started yet.";
            StatusLabel.TextColor = Colors.Red;
            StatusLabel.IsVisible = true;
            return;
        }

        try
        {
            commuteEndLocation = await Geolocation.Default.GetLocationAsync();
            if (commuteEndLocation != null && commuteStartLocation != null)
            {
                isCommuteStarted = false;
                var distance = Location.CalculateDistance(commuteStartLocation, commuteEndLocation, DistanceUnits.Kilometers);
                totalDistanceTravelled += distance;
                StatusLabel.Text = $"Commute ended. Distance travelled: {distance:F2} km";
                StatusLabel.TextColor = Colors.Green;
                StatusLabel.IsVisible = true;
            }
            else
            {
                StatusLabel.Text = "Unable to get location.";
                StatusLabel.TextColor = Colors.Red;
                StatusLabel.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = $"Error: {ex.Message}";
            StatusLabel.TextColor = Colors.Red;
            StatusLabel.IsVisible = true;
        }
    }

    private void OnStartBreakClicked(object sender, EventArgs e)
    {
        if (isBreakStarted)
        {
            StatusLabel.Text = "Break already started.";
            StatusLabel.TextColor = Colors.Red;
            StatusLabel.IsVisible = true;
            return;
        }

        isBreakStarted = true;
        StatusLabel.Text = "Break started.";
        StatusLabel.TextColor = Colors.Green;
        StatusLabel.IsVisible = true;
    }

    private void OnEndBreakClicked(object sender, EventArgs e)
    {
        if (!isBreakStarted)
        {
            StatusLabel.Text = "Break not started yet.";
            StatusLabel.TextColor = Colors.Red;
            StatusLabel.IsVisible = true;
            return;
        }

        isBreakStarted = false;
        StatusLabel.Text = "Break ended.";
        StatusLabel.TextColor = Colors.Green;
        StatusLabel.IsVisible = true;
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        // TODO: Send logout event and total distance to backend

        await Navigation.PopToRootAsync();
    }
}
