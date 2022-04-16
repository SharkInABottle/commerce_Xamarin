using commerce.Models;
using commerce.Pages.Profile;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MySales : ContentPage
    {

        public MySales()
        {
            none = new Label();
            try
            {
                var mySales = Services.sales.Where(x => x.UserClassId == activeUser.Id).OrderByDescending(x => x.CreatedDate);
                if (mySales.Count() > 0)
                {
                    none.IsVisible = false;
                    BindingContext = mySales;
                }
            }
            catch
            {
                none.IsVisible = true;
            }
            InitializeComponent();
        }
        private async void SaleDetails(object sender, ItemTappedEventArgs e)
        {
            Sale sale = e.Item as Sale;
            await Navigation.PushAsync(new OwnSaleDetails(sale.Id));

        }


    }
}