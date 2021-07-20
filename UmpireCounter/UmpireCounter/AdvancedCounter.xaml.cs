using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UmpireCounter
{
    public partial class AdvancedCounter : ContentPage, INotifyPropertyChanged
    {
        /*ItemsViewModel _viewModel; */

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

        async void increaseOversClicked(object sender, EventArgs e)
        {
            Score.IncreaseBalls();
            UpdateDisplay();
        }

        async void decreaseOversClicked(object sender, EventArgs e)
        {
            Score.DecreaseBalls();
            UpdateDisplay();
        }

        async void increaseWicketsClicked(object sender, EventArgs e)
        {
            Score.IncreaseWickets();
            UpdateDisplay();
        }

        async void decreaseWicketsClicked(object sender, EventArgs e)
        {
            Score.DecreaseWickets();
            UpdateDisplay();
        }

        async void increaseTotalClicked(object sender, EventArgs e)
        {
            Score.IncreaseTotal();
            UpdateDisplay();
        }

        async void decreaseTotalClicked(object sender, EventArgs e)
        {
            Score.DecreaseTotal();
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            OversHeader = Score.Overs.ToString();
            ScoreHeader = Score.Total.ToString() + "-" + Score.Wickets.ToString();
        }

        private void resetButtonClicked(object sender, EventArgs e)
        {
            TextFileStorage.resetScore();
            UpdateDisplay();
        }
    }
}