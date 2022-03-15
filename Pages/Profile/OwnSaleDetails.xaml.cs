using commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages.Profile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnSaleDetails : ContentPage
    {
        public OwnSaleDetails(Sale sale)
        {
            BindingContext = sale;
            InitializeComponent();
        }
    }
}