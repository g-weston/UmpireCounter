using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UmpireCounter
{
    public partial class BasicCounter : ContentPage, INotifyPropertyChanged
    {
        public BasicCounter()
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

        void UpdateDisplay()
        {
            OversHeader = Score.Overs.ToString();
        }
    }
}