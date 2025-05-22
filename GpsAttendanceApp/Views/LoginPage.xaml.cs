using System;
using Microsoft.Maui.Controls;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GpsAttendanceApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;
        var username = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ErrorLabel.Text = "Please enter username and password.";
            ErrorLabel.IsVisible = true;
            return;
        }

        var loginSuccess = await AuthenticateUser(username, password);
        if (loginSuccess)
        {
            // Navigate to GPS login page
            await Navigation.PushAsync(new GpsLoginPage());
        }
        else
        {
            ErrorLabel.Text = "Invalid username or password.";
            ErrorLabel.IsVisible = true;
        }
    }

    private async Task<bool> AuthenticateUser(string username, string password)
    {
        try
        {
            using var client = new HttpClient();
            var loginData = new { username, password };
            var json = JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Replace with your backend login API URL
            var response = await client.PostAsync("https://your-backend-api.com/api/login", content);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
