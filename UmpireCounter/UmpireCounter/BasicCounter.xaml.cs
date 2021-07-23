﻿using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UmpireCounter
{
    public partial class BasicCounter : ContentPage, INotifyPropertyChanged
    {
        public BasicCounter()
        {
            UpdateDisplay();
            InitializeComponent();
            BindingContext = this;
            
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

        void UpdateDisplay()
        {
            OversHeader = Score.Overs.ToString();
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