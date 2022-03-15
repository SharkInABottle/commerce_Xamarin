using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace commerce.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        bool themeBool = Preferences.Get("DarkTheme", false);
        public Settings()
        {

            darkMode = new Xamarin.Forms.Switch();
            darkMode.IsToggled = Preferences.Get("DarkTheme", false);
            InitializeComponent();
        }



        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Application.Current.UserAppTheme=(!themeBool ? OSAppTheme.Light : OSAppTheme.Dark);
            Preferences.Set("DarkTheme", !themeBool);
            themeBool = !themeBool;
            Console.WriteLine($"application theme value is set to {themeBool}");

        }
    }
}