using Xamarin.Forms;

namespace commerce
{
    public partial class App : Application
    {
        public App()
        {
            UserAppTheme = OSAppTheme.Light;

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
