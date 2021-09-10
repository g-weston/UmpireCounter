using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UmpireCounter
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            TextFileStorage.ReadScore();
            TextFileStorage.ReadSettings();
            MainPage = new AppShell();
            //Score.AnyButtonPressed = false;
        }

        protected override void OnStart()
        {
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
