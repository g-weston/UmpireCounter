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

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            //await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
