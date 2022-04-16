using commerce.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace commerce.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profil : ContentPage
    {
        public Profil()
        {
            
            InitializeComponent();
            LogInStatus();
        }

        private async void SignUpClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SignUp());
        }

        private async void SignInClicked(object sender, EventArgs e)
        {
            try
            {
                if (UserNameSign.Text.Length < 4 || password.Text.Length < 5)
                {
                    await DisplayAlert("error", "username or password error ", "ok");
                }
                else
                {
                    (sender as Button).IsEnabled = false;
                    var x = await AuthServices.ConnectUser(UserNameSign.Text, password.Text);
                    (sender as Button).IsEnabled = true;
                    if(x=="success") await Services.UserClaims();
                    await DisplayAlert("Result", x, "ok");
                    LogInStatus();
                }
            }
            catch
            {
                await DisplayAlert("error", "please enter username or password :", "ok");
            }
        }
        private void LogInStatus()
        {
            if (AuthServices.Client.IsActive)
            {
                ProfileStack.IsVisible = true;
                signInStack.IsVisible = false;
                UserName.Text = activeUser.UserName;
                phoneNumber.Text = activeUser.PhoneNumber.ToString();
                email.Text = activeUser.Email;
            }
            else
            {
                ProfileStack.IsVisible = false;
                signInStack.IsVisible = true;
            }
        }

        private void LogOutClicked(object sender, EventArgs e)
        {
            AuthServices.Client.LogOut();
            LogInStatus();
        }
    }
}