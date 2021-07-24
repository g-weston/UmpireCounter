using System;
using System.ComponentModel;
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
            UpdateDisplay();
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

        void IncreaseOversClicked(object sender, EventArgs e)
        {
            Score.IncreaseBalls();
            SettingsPage.VibrateChecker();
            UpdateDisplay();
        }

        void DecreaseOversClicked(object sender, EventArgs e)
        {
            Score.DecreaseBalls();
            SettingsPage.VibrateChecker();
            UpdateDisplay();
        }

        void IncreaseWicketsClicked(object sender, EventArgs e)
        {
            Score.IncreaseWickets();
            SettingsPage.VibrateChecker();
            UpdateDisplay();
        }

        void DecreaseWicketsClicked(object sender, EventArgs e)
        {
            Score.DecreaseWickets();
            SettingsPage.VibrateChecker();
            UpdateDisplay();
        }

        void IncreaseTotalClicked(object sender, EventArgs e)
        {
            Score.IncreaseTotal();
            SettingsPage.VibrateChecker();
            UpdateDisplay();
        }

        void DecreaseTotalClicked(object sender, EventArgs e)
        {
            Score.DecreaseTotal();
            SettingsPage.VibrateChecker();
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            OversHeader = Score.OversCompleted.ToString() + "." + Score.ValidDeliveriesInOver.ToString();
            ScoreHeader = Score.Total.ToString() + "-" + Score.Wickets.ToString();
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