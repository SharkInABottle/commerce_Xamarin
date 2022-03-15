using commerce.Models;
using commerce.Pages.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using commerce;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MySales : ContentPage
    {
        
        public MySales()
        {
            BindingContext = Sale.sales;
            InitializeComponent();
        }
        private async void SaleDetails(object sender, ItemTappedEventArgs e)
        {
            Sale sale = e.Item as Sale;
            await Navigation.PushAsync(new OwnSaleDetails(sale));
            
        }
    }
}