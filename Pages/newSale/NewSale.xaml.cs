using commerce.Models;
using Imagekit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages.newSale
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewSale : ContentPage
    {
        public NewSaleModel sale = new NewSaleModel();
        public ObservableCollection<Image> images = new ObservableCollection<Image>();
        public List<byte[]> imagesBytes=new List<byte[]>();
        public NewSale()
        {
            InitializeComponent();
            sale.Images = new List<Images>();
            selectableRegion.ItemsSource = RegionsList.Regions;
            selectableCategories.ItemsSource = Categories.CategoriesList;
            carousel.BindingContext = images;
            EntriesStack.BindingContext = sale;
        }

        private async void SaveClicked(object sender, EventArgs e)
        {
            saveBtn.IsEnabled = false;
            discardBtn.IsEnabled = false;

            SaveSale();
            //await DisplayAlert("test", $"{sale.Title} \n {sale.Body} \n {sale.Categorie}\n {imagesBytes.Count}", "ok");

            /*DisplayAlert("alert", ex.Message, "ok");
            var x=await Services.GetAuthImagekit();
            var y = await SecureStorage.GetAsync("AccessTokenUser");
            bodyeditor.Text = y;
            await DisplayAlert("ttt", $"{x.signature}", "ok");*/

        }

        private async void DiscardClicked(object sender, EventArgs e)
        {
            bool x = await DisplayAlert("warning", "discard and leave ?", "yes", "no");
            if (x) await Navigation.PopModalAsync();

        }
        private async void SaveSale()
        {
            var current = Connectivity.NetworkAccess;
            if (current == NetworkAccess.Internet)
            {
                if (!await UploadImages())
                {
                    return;
                    saveBtn.IsEnabled = true;
                    discardBtn.IsEnabled = true;
                }

                await DisplayAlert("tt",$"number of images :{sale.Images.Count} {sale.Title} {sale.Body} {sale.Images[0].Id} {sale.Images[0].Url} ", "ok");
                if (await Services.PostSale(sale))
                {
                    await DisplayAlert("Success", "New item is added", "ok");
                    saveBtn.IsEnabled = true;
                    discardBtn.IsEnabled = true;
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Couldn't post your sale!please try again", "ok");
                    saveBtn.IsEnabled = true;
                    discardBtn.IsEnabled = true;
                }
            }
            else if (current != NetworkAccess.Internet)
            {
                await DisplayAlert("error", "There is no internet, please try again when u have internet access", "ok");
                saveBtn.IsEnabled = true;
                discardBtn.IsEnabled = true;
            }
            
        }
        private async void AddImageClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;
            ObservableCollection<FileResult> file;
            bool fileOutSized;
            try
            {

                file = new ObservableCollection<FileResult>(await FilePicker.PickMultipleAsync(new PickOptions { FileTypes =FilePickerFileType.Images }));
                images.Clear();
                imagesBytes.Clear();
                //images.Add(new Image { Source = file.FullPath});
                foreach (FileResult fileResult in file)
                {
                    Stream stream = await fileResult.OpenReadAsync();

                    ImageSource x = ImageSource.FromStream(() => stream);
                    MemoryStream x1 = new MemoryStream();
                    stream.CopyTo(x1);
                    if (x1.ToArray().Length > (24 * 1024 * 1024))
                    {
                        fileOutSized = true;
                        continue;
                    }
                    
                    imagesBytes.Add(x1.ToArray());
                    images.Add(new Image { Source = fileResult.FullPath });
                    x1.Dispose();
                    stream.Dispose();

                }

            }
            catch
            {

            }
            (sender as Button).IsEnabled = true;
        }
        public async Task<bool> UploadImages()
        {
            
            foreach (byte[] x in imagesBytes)
            {
                try
                {
                    var uploadedImage = await Services.UploadImage(x, "test");
                    if (uploadedImage.StatusCode < 299)
                    {
                        sale.Images.Add(new Images(uploadedImage));
                        //stack1.Children.Add(new Image { Source = uploadedImage.URL, Aspect = Aspect.AspectFit });
                        
                    }
                    else
                    {
                        await DisplayAlert("error", $"image didn't upload error: {uploadedImage.Message} statusCode: {uploadedImage.StatusCode}", "ok");
                        return false;

                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("error", $"image didn't upload exception: {ex.Message} {x.Length}", "ok");
                    return false;
                }
                
            }
            return true;
        }
    }
}