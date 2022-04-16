using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages.SaleDetails
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageView : ContentPage
	{
		public ImageView (ImageSource image)
		{
			BindingContext = image;
			InitializeComponent ();
		}

        private async void ReturnClicked(object sender, EventArgs e)
        {
			await Navigation.PopModalAsync();
        }
    }
}