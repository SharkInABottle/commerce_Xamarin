using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace commerce.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public bool themeBool
        {
            get
            {
                return Preferences.Get("DarkTheme", false);
            }
            set
            {
                Preferences.Set("DarkTheme", value); Application.Current.UserAppTheme = (!value ? OSAppTheme.Light : OSAppTheme.Dark);
                DisplayAlert("alert!", $"application theme {Application.Current.RequestedTheme} value is set to {Preferences.Get("DarkTheme", false)}", "ok");
            }
        }
        public Settings()
        {
            
            InitializeComponent();
            BindingContext = this;

        }





    }
}