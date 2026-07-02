namespace MAUI_JSON_TEST;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        UserAppTheme = AppTheme.Dark;

        MainPage = new AppShell();
    }
}