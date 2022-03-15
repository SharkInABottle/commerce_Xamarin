using commerce.Models;
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
    }
}