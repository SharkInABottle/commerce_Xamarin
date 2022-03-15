using commerce.Models;
using commerce.Pages;
using commerce.Pages.Profile;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace commerce
{
    public partial class MainPage : ContentPage
    {
        public List<Sale> sales;
        public MainPage()
        {
            sales = Sale.sales;
            sales.AddRange(sales);
            BindingContext = sales;
            InitializeComponent();
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

            await Navigation.PushAsync(new SaleDetail(sale));

        }

        private async void ListView_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            yahalo1.EndRefresh();
        }

        private void SearchClicked(object sender, EventArgs e)
        {
            
        }
    }
}
