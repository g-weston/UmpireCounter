using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UmpireCounter
{
    public partial class AdvancedCounter : ContentPage, INotifyPropertyChanged
    {
        public AdvancedCounter()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            UpdateDisplay();
            DeviceDisplay.KeepScreenOn = true;
        }

        private string oversHeader = Score.OversCompleted.ToString() + "." + Score.ValidDeliveriesInOver.ToString();
        public string OversHeader
        {
            get => oversHeader;
            set
            {
                if (oversHeader != value)
                {
                    oversHeader = value;
                    this.OnPropertyChanged("OversHeader");
                }
            }
        }

        private string scoreHeader = Score.Total.ToString() + "-" + Score.Wickets.ToString();
        public string ScoreHeader
        {
            get => scoreHeader;
            set
            {
                if (scoreHeader != value)
                {
                    scoreHeader = value;
                    this.OnPropertyChanged("ScoreHeader");
                }
            }
        }

        private string timeHeader = "Innings time: " + Score.InningsTime;
        public string TimeHeader
        {
            get => timeHeader;
            set
            {
                if (timeHeader != value)
                {
                    timeHeader = value;
                    this.OnPropertyChanged("TimeHeader");
                }
            }
        }

        void IncreaseOversClicked(object sender, EventArgs e)
        {
            Score.IncreaseBalls();
            SettingsPage.VibrateChecker();
            Score.UpdateInningsTimer();
            UpdateDisplay();
        }

        void DecreaseOversClicked(object sender, EventArgs e)
        {
            Score.DecreaseBalls();
            SettingsPage.VibrateChecker();
            Score.UpdateInningsTimer();
            UpdateDisplay();
        }

        void IncreaseWicketsClicked(object sender, EventArgs e)
        {
            Score.IncreaseWickets();
            SettingsPage.VibrateChecker();
            Score.UpdateInningsTimer();
            UpdateDisplay();
        }

        void DecreaseWicketsClicked(object sender, EventArgs e)
        {
            Score.DecreaseWickets();
            SettingsPage.VibrateChecker();
            Score.UpdateInningsTimer();
            UpdateDisplay();
        }

        void IncreaseTotalClicked(object sender, EventArgs e)
        {
            Score.IncreaseTotal();
            SettingsPage.VibrateChecker();
            Score.UpdateInningsTimer();
            UpdateDisplay();
        }

        void DecreaseTotalClicked(object sender, EventArgs e)
        {
            Score.DecreaseTotal();
            SettingsPage.VibrateChecker();
            Score.UpdateInningsTimer();
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            OversHeader = Score.OversCompleted.ToString() + "." + Score.ValidDeliveriesInOver.ToString();
            ScoreHeader = Score.Total.ToString() + "-" + Score.Wickets.ToString();
            TimeHeader = "Innings time: " + Score.InningsTime;

            if (!SettingsPage.TimerOnOff)
            {
                TimeHeaderLabel.IsVisible = false;
            }
            else if (SettingsPage.TimerOnOff)
            {
                TimeHeaderLabel.IsVisible = true;
            }
        }

        public async void ResetButtonClicked(object sender, EventArgs e)
        {
            bool resetConfirm = await DisplayAlert("Reset", "Are you sure you want to reset?", "Yes", "Cancel");
            if (resetConfirm)
            {
                TextFileStorage.ResetScore();
                UpdateDisplay();
            }
        }
    }
}