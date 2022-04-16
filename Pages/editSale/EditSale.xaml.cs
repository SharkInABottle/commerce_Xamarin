using commerce.Models;
using CommerceApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages.editSale
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditSale : ContentPage
    {
        EditSaleClass editSale ;
        public EditSale(Sale sale)
        {
            selectablelistview = new Picker();
            selectableCategories = new Picker();
            InitializeComponent();
            selectablelistview.ItemsSource = RegionsList.Regions;
            selectableCategories.ItemsSource = Categories.CategoriesList;         
            
            editSale = new EditSaleClass
            {            
                Id = sale.Id,
                Title = sale.Title,
                Body = sale.Body,
                Region = sale.Region,
                Price = sale.Price,
                Categorie = sale.Categorie,
                UserClassId = sale.UserClassId
            };
            

            BindingContext = editSale;
        }
        private async void SaveClicked(object sender, EventArgs e)
        {

            if (editSale == null) await DisplayAlert("error", "null sale", "ok");
            if (await Services.PutSale(editSale))
            {
                await DisplayAlert("Done", "Modification was successful", "Ok");
                Services.sales.Remove(Services.sales.Where(i => i.Id == editSale.Id).Single());
                Services.sales.Add(await Services.GetSale(editSale.Id));
                Services.sales = new ObservableCollection<Sale>(Services.sales.OrderByDescending(x => x.CreatedDate).ToList());
                
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("erreur", "modification error", "ok");
            }

        }

        private void DiscardClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}