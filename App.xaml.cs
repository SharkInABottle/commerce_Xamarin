using commerce.Models;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace commerce
{
    public partial class App : Application
    {
        public App()
        {

            InitializeComponent();
            

            MainPage = new NavigationPage(new MainPage()) { BarBackgroundColor = Color.Chocolate };

                
            

            
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
