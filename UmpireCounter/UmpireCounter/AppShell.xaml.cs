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

        public static bool AboutPageShowing;

        protected override bool OnBackButtonPressed()
        {
            if (AboutPageShowing)
            {
                AboutPageShowing = false;
                return base.OnBackButtonPressed();
            }
            else
            {
                NavigateBackCounter();
                return true;
            }
        }
        
        public async void NavigateBackCounter()
        {
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
            AboutPageShowing = true;
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new AboutPage());
        }
    }
}
