using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using UmpireCounter.Droid;


[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
public class SplashActivity : AppCompatActivity
{
    static readonly string TAG = "X:" + typeof(SplashActivity).Name;

    public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
    {
        base.OnCreate(savedInstanceState, persistentState);
        Log.Debug(TAG, "SplashActivity.OnCreate");
    }

    public override void OnBackPressed() { }

    protected override void OnResume()
    {
        base.OnResume();
        Task startupWork = new Task(() => { });
        startupWork.Start();
        StartActivity(new Intent(Application.Context, typeof(MainActivity)));
    }
}