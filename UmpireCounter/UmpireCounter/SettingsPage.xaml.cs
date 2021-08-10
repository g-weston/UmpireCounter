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

        private string ballsInOverPlaceHolder = Score.BallsInOver.ToString();
        public string BallsInOverPlaceHolder
        {
            get => ballsInOverPlaceHolder;
            set
            {
                if (ballsInOverPlaceHolder != value)
                {
                    ballsInOverPlaceHolder = value;
                    this.OnPropertyChanged("BallsInOverPlaceHolder");
                }
            }
        }

        public static bool Vibrate { get; set; }
        public static bool TimerOnOff { get; set; }
        public static bool LoadInPage { get; set; }

        void VibrateButtonsSwitch(object sender, ToggledEventArgs e)
        {
            if (e.Value) SettingsPage.Vibrate = true;
            if (!e.Value) SettingsPage.Vibrate = false;
        }

        void TimerButtonsSwitch(object sender, ToggledEventArgs e)
        {
            if (e.Value) SettingsPage.TimerOnOff = true;
            if (!e.Value) SettingsPage.TimerOnOff = false;
        }

        void LoadInPageSwitch(object sender, ToggledEventArgs e)
        {
            if (e.Value) SettingsPage.LoadInPage = true;
            if (!e.Value) SettingsPage.LoadInPage = false;
        }

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
                TextFileStorage.WriteSettings();
                await DisplayAlert("Settings",
                    "Your settings have been updated.", "Ok");
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