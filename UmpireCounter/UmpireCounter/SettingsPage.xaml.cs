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
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateSettingsDisplay();
        }
                
        public static bool Vibrate { get; set; }
        public static bool TimerOnOff { get; set; }
        public static bool LoadInPage { get; set; }
        public static bool DisplayWickets { get; set; }
        public static bool DisplayRuns { get; set; }

        private async void UpdateSettingsClicked(object sender, System.EventArgs e)
        {
            bool ballNumber = false;
            bool oversZero = false;
            bool updateBalls = false;
            bool errorUpdate = false;
            if (int.TryParse(EntryBallsInOver.Text, out int ballsNumOver))
            {

                if (ballsNumOver != Score.BallsInOver)
                {
                    if (ballsNumOver >= 1)
                    {

                        ballNumber = true;
                        if (Score.OversCompleted == 0 && Score.ValidDeliveriesInOver == 0)
                        {
                            oversZero = true;
                        }
                        else
                        {
                            errorUpdate = true;
                            await DisplayAlert("Error",
                                "Innings currently in progress, please reset the innings before changing the number of balls in an over",
                                "Ok");
                        }

                    }
                }
                else
                {
                    updateBalls = false;
                }
            }
            else if (String.IsNullOrWhiteSpace(EntryBallsInOver.Text))
            {
                updateBalls = false;
            }
            else
            {
                errorUpdate = false;
                await DisplayAlert("Error",
                    "Please enter a valid number (more than 0) in the number of balls in an over entry.", "Ok");
            }

            if (ballNumber && oversZero)
            {
                updateBalls = true;
            }
            
            if (updateBalls)
            {
                Score.BallsInOver = ballsNumOver;
            }

            if (!errorUpdate)
            {
                if (VibrateSwitch.IsToggled) SettingsPage.Vibrate = true;
                else SettingsPage.Vibrate = false;

                if (TimerSwitch.IsToggled) SettingsPage.TimerOnOff = true;
                else SettingsPage.TimerOnOff = false;

                if (DisplayRunsSwitch.IsToggled) SettingsPage.DisplayRuns = true;
                else SettingsPage.DisplayRuns = false;

                if (DisplayWicketsSwitch.IsToggled) SettingsPage.DisplayWickets = true;
                else SettingsPage.DisplayWickets = false;

                TextFileStorage.WriteSettings();
                await DisplayAlert("Settings",
                    "Your settings have been updated.", "Ok");
            }

            UpdateSettingsDisplay();
        }

        public static void VibrateChecker()
        {
            if (Vibrate)
            {
                var duration = TimeSpan.FromSeconds(0.1);
                Vibration.Vibrate(duration);
            }
        }

        public void UpdateSettingsDisplay()
        {
            if (SettingsPage.Vibrate)
            {
                VibrateSwitch.IsToggled = true;
            }
            else
            {
                VibrateSwitch.IsToggled = false;
            }

            if (SettingsPage.TimerOnOff)
            {
                TimerSwitch.IsToggled = true;
            }
            else
            {
                TimerSwitch.IsToggled = false;
            }

            if (SettingsPage.DisplayRuns)
            {
                DisplayRunsSwitch.IsToggled = true;
            }
            else
            {
                DisplayRunsSwitch.IsToggled = false;
            }

            if (SettingsPage.DisplayWickets)
            {
                DisplayWicketsSwitch.IsToggled = true;
            }
            else
            {
                DisplayWicketsSwitch.IsToggled = false;
            }

            EntryBallsInOver.Placeholder = Score.BallsInOver.ToString();
        }

        public async void BackCommand()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}