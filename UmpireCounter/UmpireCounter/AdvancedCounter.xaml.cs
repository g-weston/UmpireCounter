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

        private string oversHeader = Score.Overs.ToString();
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
            UpdateDisplay();
        }

        void DecreaseOversClicked(object sender, EventArgs e)
        {
            Score.DecreaseBalls();
            UpdateDisplay();
        }

        void IncreaseWicketsClicked(object sender, EventArgs e)
        {
            Score.IncreaseWickets();
            UpdateDisplay();
        }

        void DecreaseWicketsClicked(object sender, EventArgs e)
        {
            Score.DecreaseWickets();
            UpdateDisplay();
        }

        void IncreaseTotalClicked(object sender, EventArgs e)
        {
            Score.IncreaseTotal();
            UpdateDisplay();
        }

        void DecreaseTotalClicked(object sender, EventArgs e)
        {
            Score.DecreaseTotal();
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            OversHeader = Score.Overs.ToString();
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