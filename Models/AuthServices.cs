using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace commerce.Models
{
    static class AuthServices
    {

        static readonly HttpClient client = new HttpClient();
        static string BaseAdress = Settings.Settings.BaseAdress;
        static string RegisterAdress = Settings.Settings.RegisterAdress;
        public static async Task<string> RegisterUser(RegisterModel registerModel)
        {
            client.SetBearerToken(ClientAnonymous.AccessToken);
            HttpResponseMessage resp = await client.PostAsJsonAsync(RegisterAdress, registerModel);
            resp.EnsureSuccessStatusCode();
            if (resp.IsSuccessStatusCode) return "success";
            else return "error";
        }
        public static async Task<string> Connect()
        {

            if (Client.IsActive)
            {
                if (IsExpiringUser()) await ConnectUser("", "");
                return "usersuccess";
            }
            return "anonymous" + await ClientAnonymous.GetToken();
        }
        
        public static async Task<string> ConnectUser(string userName, string password)
        {
            if ((!Client.IsActive) || IsExpiringUser())
            {
                if (IsExpiringUser())
                {
                    userName = await SecureStorage.GetAsync("UserName");
                    password = await SecureStorage.GetAsync("Password");
                }

                var y = await Client.GetToken(userName, password);
                if (y == "success")
                {
                    Client.IsActive = true;
                    ClientAnonymous.IsActive = false;
                    return "success";
                }
                return y;
            }
            else if (Client.IsActive)
            {
                return "success";
            }
            return "error";
        }

        public static class ClientAnonymous
        {
            public static string ClientId = Settings.Settings.ClientAnonymousId;
            public static string ClientSecret = Settings.Settings.ClientAnonymousSecret;
            public static string scope = Settings.Settings.scopeAnonym;
            public static string AccessToken { get; set; }
            public static bool IsActive { get => Preferences.Get("IsAnonymous", false); set => Preferences.Set("IsAnonymous", value); }
            public static async Task<string> GetToken()
            {

                var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address = BaseAdress,
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,
                    Scope = scope
                });
                if (tokenResponse.IsError)
                {
                    return tokenResponse.Error;
                }
                AccessToken = tokenResponse.AccessToken;
                IsActive = true;
                return "success";
            }
            /*public static async Task<string> GetRefreshToken()
            {

                var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
                {
                    Address = BaseAdress,
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,

                    Scope = scope
                });
                if (tokenResponse.IsError)
                {
                    return tokenResponse.Error;
                }
                AccessToken = tokenResponse.AccessToken;
                IsActive = true;
                
                return "success";
            }*/
        }
        public static class Client
        {
            public static string ClientId = Settings.Settings.ClientId;
            public static string ClientSecret = Settings.Settings.ClientSecret;
            public static string scope = Settings.Settings.scope;
            
            public static string RefreshToken { get => Preferences.Get("RefreshTokenUser", ""); set => Preferences.Set("RefreshTokenUser", value); }
            public static DateTime AuthenTime { get => Preferences.Get("AuthenTimeUser", DateTime.Now); set => Preferences.Set("AuthenTimeUser", value); }
            public static bool IsActive { get => Preferences.Get("IsUser", false); set => Preferences.Set("IsUser", value); }
            public static int ExpirationTimeSpan { get => Preferences.Get("ExpirationTimeSpan", 1300000); set => Preferences.Set("ExpirationTimeSpan", value); }

            public static async Task<string> GetToken(string userName, string password)
            {

                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = BaseAdress,
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,
                    UserName = userName,
                    Password = password,
                    Scope = scope
                });
                if (tokenResponse.IsError)
                {
                    return tokenResponse.Error;
                }
                await SecureStorage.SetAsync("UserName", userName);
                await SecureStorage.SetAsync("Password", password);
                await SecureStorage.SetAsync("AccessTokenUser", tokenResponse.AccessToken);
                //AccessToken = tokenResponse.AccessToken;
                ExpirationTimeSpan = tokenResponse.ExpiresIn;
                AuthenTime = DateTime.Now;
                RefreshToken = tokenResponse.RefreshToken;
                IsActive = true;
                return "success";
            }            
            public static void Refresh()
            {

            }
            public static async void LogOut()
            {
                IsActive = false;
                SecureStorage.RemoveAll();
                RefreshToken = null;
                IsActive = false;
                ClientAnonymous.IsActive = false;
                
            }
        }
        /*public static bool IsExpiringAnonym()
        {

            if (ClientAnonymous.AuthenTime != null)
            {
                int x = (int)DateTime.Now.Subtract(ClientAnonymous.AuthenTime).TotalSeconds;
                return (ClientAnonymous.ExpirationTimeSpan - x) <= 3600;
            }
            return false;
        }*/
        public static bool IsExpiringUser()
        {

            if (Client.AuthenTime != null)
            {
                int x = (int)DateTime.Now.Subtract(Client.AuthenTime).TotalSeconds;
                return (Client.ExpirationTimeSpan - x) <= 3600;
            }
            return false;
        }


    }
}
