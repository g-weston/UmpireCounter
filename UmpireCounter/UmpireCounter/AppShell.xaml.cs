using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        
        protected override bool OnBackButtonPressed()
        {
            NavigateBackCounter();
            return true;
        }
        
        public async void NavigateBackCounter()
        {
            await Shell.Current.GoToAsync("////SettingsPage");
            await Shell.Current.GoToAsync("////AdvancedCounter");
        }

        /*
        protected override bool OnBackButtonPressed()
        {
            var page = (Shell.Current?.CurrentItem?.CurrentItem as IShellSectionController)?.PresentedPage;
            if (page.SendBackButtonPressed())
            {
                NavigateBackCounter();
                return true;
            }
            else
                return base.OnBackButtonPressed();
        }*/
        
        private async void AboutButtonClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new AboutPage());
        }
    }
}
