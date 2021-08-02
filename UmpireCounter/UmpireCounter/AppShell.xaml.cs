using System;
using System.Collections.Generic;
//using UmpireCounter.ViewModels;
using Xamarin.Forms;

namespace UmpireCounter
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void AboutButtonClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new AboutPage());
        }
    }
}
