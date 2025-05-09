using Android.App;
using Android.Content.PM;
using Android.OS;

namespace MAUI_JSON_TEST;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode |
                           ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        // note 設置安裝的全透明任務欄
        Window.SetFlags(Android.Views.WindowManagerFlags.TranslucentNavigation,
            Android.Views.WindowManagerFlags.TranslucentNavigation);
        //note 設置狀態欄，導航欄顏色為透明
        Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
        Window.SetNavigationBarColor(Android.Graphics.Color.Transparent);
        
        base.OnCreate(savedInstanceState);
    }
}