using commerce.Models;
using commerce.Pages.SaleDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SaleDetail : ContentPage
    {
        public SaleDetail(Sale sale)
        {
            BindingContext = sale;
            InitializeComponent();
            
        }

        private void ImageView(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ImageView((carousel.CurrentItem as Images).Url));
        }

        private async void ReportClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Signaler", "Merci pour signaler", "ok");
            await Navigation.PopModalAsync();
        }

        private async void ReturnClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}