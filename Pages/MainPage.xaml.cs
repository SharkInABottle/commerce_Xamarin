using commerce.Models;
using commerce.Pages;
using commerce.Pages.Profile;
using Imagekit;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace commerce
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {


            InitializeComponent();

            try
            {
                GetSalesBinded();

            }
            catch
            {
                DisplayAlert("error", "network error please try again", "ok");
            }




        }
        public async void testImageKit()
        {
            AuthParamResponse x = await Services.GetAuthImagekit();
            await DisplayAlert(x.expire, "Token: " + x.token + "Signature: " + x.signature, "ok");
        }
        private async void GetSalesBinded()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                yahalo1.IsPullToRefreshEnabled = false;
                yahalo1.IsRefreshing = true;
                bool x = await Services.GetSales();
                if (x)
                {
                    BindingContext = Services.sales;

                    yahalo1.IsPullToRefreshEnabled = true;

                    yahalo1.EndRefresh();
                }
                else
                {
                    yahalo1.IsPullToRefreshEnabled = true;
                    yahalo1.EndRefresh();
                    await DisplayAlert("error", "please try again", "ok");
                }
            }
            else
            {
                yahalo1.EndRefresh();
                await DisplayAlert("error", "There is no internet, please try again when u have internet access", "ok");
                yahalo1.IsPullToRefreshEnabled = true;
            }

        }
        private async void Settings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Settings());
        }
        private async void Profile(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfileTabbed());
        }
        private async void SaleDetails(object sender, ItemTappedEventArgs e)
        {
            Sale sale = e.Item as Sale;

            await Navigation.PushModalAsync(new SaleDetail(sale));

        }
        private void ListView_Refreshing(object sender, EventArgs e)
        {
            GetSalesBinded();

        }
        private void SearchClicked(object sender, EventArgs e)
        {
            testImageKit();
        }
        private async void MessagesTab(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfileTabbed("messages"));

        }
        private async void CreateNew(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new Pages.newSale.NewSale());
            if (AuthServices.Client.IsActive) await Navigation.PushModalAsync(new Pages.newSale.NewSale());
            else if (await DisplayAlert("Alerte", "Vous devez vous connecter/inscrire afin de publier des annonces", "signIn/Up", "Ok")) await Navigation.PushAsync(new ProfileTabbed());
        }
        private async void FilterButton(object sender, EventArgs e)
        {
            //testImageKit();
            //var x = await AuthServices.ConnectAnonymous();
            //await DisplayAlert("Connecting",x+ AuthServices.ClientAnonymous.AccessToken, "ok");

            //await DisplayAlert("First item", Services.sales[0].UserClassId, "ok");
        }
    }
}
