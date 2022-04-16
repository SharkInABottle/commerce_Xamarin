using commerce.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnSaleDetails : ContentPage
    {
        public OwnSaleDetails(int id)
        {
            
            BindingContext = Services.sales.Where(x=>x.Id==id).FirstOrDefault();
            InitializeComponent();
        }
        private void EditSale(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new editSale.EditSale(this.BindingContext as Sale));
        }
        private void ReturnClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
        private async void DeleteSale(object sender, EventArgs e)
        {
            if (await DisplayAlert("ALERT", "Do you want to delete this item permanently?", "yes", "cancel"))
            {
                Sale x = BindingContext as Sale;
                if (await Services.DeleteSale(x.Id))
                {
                    await Navigation.PopAsync();
                    await DisplayAlert("Done", "Successfully deleted", "Ok");
                }
                else
                    await DisplayAlert("ALERT", $"Something is wrong please{x.Id} try again", "Ok");
            }
        }

    }
}