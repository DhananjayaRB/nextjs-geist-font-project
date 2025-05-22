using Microsoft.Maui.Controls;

namespace GpsAttendanceApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new Views.LoginPage());
    }
}
