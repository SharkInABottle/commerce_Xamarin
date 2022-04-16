using commerce.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        public RegisterModel registerModel = new RegisterModel();
        public SignUp()
        {
            BindingContext = registerModel;
            InitializeComponent();
        }

        private async void CancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void SignUpClicked(object sender, EventArgs e)
        {
            string x;
            if (AuthServices.ClientAnonymous.IsActive && registerModel != null)
            {
                x = await AuthServices.RegisterUser(registerModel);
                if (x == "success")
                {
                    await DisplayAlert("Success", "Successfully registered", "ok");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Something is wrong plz try again later"+x, "Ok");
                }
            }
            else if(registerModel == null)
            {
                await DisplayAlert("Error", "User info is empty plz enter valid informations", "Ok");
            }
            else
            {
                await DisplayAlert("Error", "Error", "Ok");

            }

        }
    }
}