using Pojo;

namespace MAUI_JSON_TEST;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }
    private async void ChoseItem(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (e.CurrentSelection.FirstOrDefault() is Respond repo)
            {
                await Launcher.Default.OpenAsync(repo.html_url);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("error", $"lost: {ex.Message}", "OK");
        }
    }
}