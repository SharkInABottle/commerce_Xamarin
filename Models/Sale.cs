using System;
using System.Collections.Generic;
using Imagekit;
using Newtonsoft.Json;

namespace commerce.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Price { get; set; }
        public string Body { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedTime { get => Services.CreationTime(CreatedDate); }
        public List<Images> Images { get; set; }
        public string Region { get; set; }
        public string Categorie { get; set; }
        public string UserClassId { get; set; }
        public UserClass userClass { get; set; }
        public bool IsDeleted { get; set; }
        
    }
    public class Images
    {
        public string Id { get; set; }
        public string ImagePath { get; set; }
        public string Url { get; set; }
        public Images()
        {

        }
        public Images(string imagePath, string imageId, string url) => (Id,ImagePath,Url) = (imagePath,imageId,url);
        public Images(ImagekitResponse imagekit)
        {
            ImagePath = imagekit.FilePath;
            Url = imagekit.URL;
            Id = imagekit.FileId;
            
        }
    }
    public class NewSaleModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Region { get; set; }
        public int? PhoneNumber { get; set; }
        public int? Price { get; set; }
        public string Categorie { get; set; }
        public string UserClassId { get; set; }
        public List<Images> Images { get; set; }
    }
    public static class Categories
    {
        public static List<string> CategoriesList = new List<string>
        {
            "Mode Homme", "Mode Femme", "Electronique Maison", "Voitures", "scouteur", "SmartPhones", "PC portatif", "PC de bureau", "Matériels Informatique", "Jeux video et consoles", "Autre" };
    }
    static class RegionsList
    {
        public static List<string> Regions
        {
            get
            {
                return new List<string>
                {
                    "Tunis",
                    "Ariana",
                    "BenArous",
                    "Mannouba",
                    "Bizerte",
                    "Nabeul",
                    "Béja",
                    "Jendouba",
                    "Zaghouan",
                    "Siliana",
                    "LeKef",
                    "Sousse",
                    "Monastir",
                    "Mahdia",
                    "Kasserine",
                    "SidiBouzid",
                    "Kairouan",
                    "Gafsa",
                    "Sfax",
                    "Gabès",
                    "Médenine",
                    "Tozeur",
                    "Kebili",
                    "Tataouine"
                };

            }

        }
    }
    
}

