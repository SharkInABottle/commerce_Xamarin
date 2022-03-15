using System;
using System.Collections.Generic;

namespace commerce.Models
{

    public class Sale
    {
        public static readonly List<string> RegionsList = new List<string>
        {
            "Tunis", "Ariana", "BenArous", "Mannouba", "Bizerte", "Nabeul", "Béja", "Jendouba", "Zaghouan", "Siliana", "LeKef", "Sousse", "Monastir", "Mahdia", "Kasserine", "SidiBouzid", "Kairouan", "Gafsa", "Sfax", "Gabès", "Médenine", "Tozeur", "Kebili" , "Tataouine"
        };
        public static List<Sale> sales = new List<Sale>
       {
           new Sale("iphone11","6go ram,64go rom,etat neuf avec son packet",5,new string[]{"https://dadadadazdazdad","https://dadadadazdazdad","https://dadadadazdazdad"},0),
           new Sale("samsung s20","6go ram,64go rom,etat neuf avec son packet",5,new string[]{"https://dadadadazdazdad","https://dadadadazdazdad","https://dadadadazdazdad"},10),
           new Sale("huawei p40","6go ram,64go rom,etat neuf avec son packet",5,new string[]{"https://dadadadazdazdad","https://dadadadazdazdad","https://dadadadazdazdad"},5)
       };
        public static int Id { get => Id; set => Id = Id++; }
        public int id { get => id; set => id = Id; }
        public string Title { get; set; }
        public int? Price    { get ; set ; }
        public string Body { get; set; }
        public DateTime CreationTime { get; set; }
        public string Likes { get; set; }
        public List<string> ImgUrl { get; set; }
        
        public string RegionName { get; set; }
        

        public Sale(string title, string body, int likes, string[] imgUrl, int region,int price)
        {
            Title = title;
            Body = body;
            Likes = "Likes" + likes.ToString();
            ImgUrl = new List<string>(imgUrl);
            RegionName = RegionsList[region];
            Price = price;
        }
        public Sale(string title, string body, int likes, string[] imgUrl, int region)
        {
            Title = title;
            Body = body;
            Likes = "Likes" + likes.ToString();
            ImgUrl = new List<string>(imgUrl);
            RegionName = RegionsList[region];
            Price = null;
        }
    }

    //public enum Regions { Tunis, Ariana, BenArous, Mannouba, Bizerte, Nabeul, Béja, Jendouba, Zaghouan, Siliana, LeKef, Sousse, Monastir, Mahdia, Kasserine, SidiBouzid, Kairouan, Gafsa, Sfax, Gabès, Médenine, Tozeur, Kebili, Ttataouine };
}
