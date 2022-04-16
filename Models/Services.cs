using CommerceApi.Models;
using Imagekit;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
using Xamarin.Essentials;

namespace commerce.Models
{
    public static class Services
    {
        private readonly static string ApiUri = Settings.Settings.CommerceApiUri;
        private static readonly string ApiKey = Settings.Settings.ImagekitApiKey;
        static readonly string ApiEndpointImageKit = Settings.Settings.ImagekitApiEndpoint;
        static ClientImagekit imagekit = new ClientImagekit(ApiKey, ApiEndpointImageKit);
        static HttpClient client = new HttpClient()
        {
            Timeout = TimeSpan.FromSeconds(15),
            BaseAddress = new Uri(ApiUri),
        };
        public static ObservableCollection<Sale> sales,allSales;
        public static Sale sale;

        //GET ALL SALES
        public static async Task<bool> GetSales()
        {
            var x = await AuthServices.Connect();
            if (x == "anonymoussuccess")
            {
                client.SetBearerToken(AuthServices.ClientAnonymous.AccessToken);
            }else if (x == "usersuccess")
            {
                client.SetBearerToken(await SecureStorage.GetAsync("AccessTokenUser"));
            }
            try
            {
                HttpResponseMessage response = await client.GetAsync("");
                
                allSales = new ObservableCollection<Sale>((await response.Content.ReadAsAsync<ObservableCollection<Sale>>()).OrderByDescending(x => x.CreatedDate)); ;
                sales = new ObservableCollection<Sale>(allSales.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate));
                return true;
            }
            catch { sales = null; return false; }
        }
        //GET A SALE BY ID
        public static async Task<Sale> GetSale(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(id.ToString());
            if (response.IsSuccessStatusCode)
            {

                sale = await response.Content.ReadAsAsync<Sale>();
            }
            return sale;
        }
        //POST A NEW SALE
        public static async Task<bool> PostSale(NewSaleModel sale)
        {
            client.SetBearerToken(await SecureStorage.GetAsync("AccessTokenUser"));
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("", sale);
                response.EnsureSuccessStatusCode();
                sales.Add(await response.Content.ReadAsAsync<Sale>());
                sales = new ObservableCollection<Sale>(sales.Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedDate).ToList()); ;

                return true;
            }
            catch
            {
                return false;
            }
        }
        //DELETE A SALE BY ID
        public static async Task<bool> DeleteSale(int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(id.ToString());
                response.EnsureSuccessStatusCode();
                foreach (Sale x in sales)
                {
                    if (x.Id == id)
                    {
                        sales.Remove(x);


                        return true;
                    }
                }
                return false;

            }
            catch
            {
                return false;
            }
        }
        //PUT: EDIT A SALE
        public static async Task<bool> PutSale(EditSaleClass editSale)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(editSale.Id.ToString(), editSale);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
                return false;
        }
        public static async Task<AuthParamResponse> GetAuthImagekit()
        {
            var x = await SecureStorage.GetAsync("AccessTokenUser");
            client.SetBearerToken(x);

            HttpResponseMessage response = await client.GetAsync("Auth/" + (DateTimeOffset.UtcNow.ToUnixTimeSeconds() + 600).ToString() + "/" + Guid.NewGuid().ToString());
            return await response.Content.ReadAsAsync<AuthParamResponse>();
        }
        public static async Task<ImagekitResponse> UploadImage(byte[] image, string fileName)
        {
            try
            {
                var x = await GetAuthImagekit();
                ImagekitResponse resp = await imagekit
                    .FileName(fileName)
                    .UploadAsync(image, x);

                return resp;
            }
            catch
            {
                
                return null;
            }
        }
        public static string CreationTime(DateTime x)
        {
            double y =DateTime.Now.ToUniversalTime().Subtract(x.ToUniversalTime()).TotalSeconds;
            if (y < 120)
            {
                return "il y a quelque secondes";
            }
            else if (y < 7200)
            {
                return $"il y a {Math.Truncate(y / 60)} minutes";
            }else if (y < 3600 * 24*2)
            {
                return $"il y a {Math.Truncate(y / 3600)} heures";
            }else
            {
                return $"il y a {Math.Truncate(y / (3600*24))} jours";
            }
        }
        public static async Task UserClaims()
        {
            client.SetBearerToken(await SecureStorage.GetAsync("AccessTokenUser"));
            HttpResponseMessage response = await client.GetAsync("claims");
            if (response.IsSuccessStatusCode)
            {
                var user= await response.Content.ReadAsAsync<activeUserModel>();
                activeUser.UserName = user.UserName;
                activeUser.Email = user.Email;
                activeUser.PhoneNumber = user.PhoneNumber;
                activeUser.Id = user.Id;
            }
        }
    }
}
