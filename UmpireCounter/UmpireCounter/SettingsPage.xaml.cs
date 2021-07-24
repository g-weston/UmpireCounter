using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UmpireCounter
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            Score.BallsInOver = 6;
        }

        public static bool Vibrate { get; set; }

        void VibrateButtonsSwitch(object sender, ToggledEventArgs e)
        {
            if (e.Value) SettingsPage.Vibrate = true;
            if (!e.Value) SettingsPage.Vibrate = false;
        }

        private async void UpdateSettingsClicked(object sender, System.EventArgs e)
        {
            if (int.TryParse(EntryBallsInOver.Text, out int ballsNumOver) && int.Parse(EntryBallsInOver.Text) <= 10 && (int.Parse(EntryBallsInOver.Text) >= 1))
            {
                Score.BallsInOver = ballsNumOver;
                await DisplayAlert("Settings",
                    "Your settings have been updated.", "Ok");
            }
            else
            {
                await DisplayAlert("Error",
                    "Please enter a valid number (1-10) in the number of balls in an over entry.", "Ok");
            }
        }

        public static void VibrateChecker()
        {
            if (Vibrate)
            {
                var duration = TimeSpan.FromSeconds(0.1);
                Vibration.Vibrate(duration);
            }
        }
    }
}