using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace commerce.Models
{
    public class UserClass
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime RegisteredTime { get; set; }
        public int NumberOfSales { get => SalesId.Count; }
        public List<int> SalesId { get; set; }
    }
    public class RegisterModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
    }
    public class activeUserModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int PhoneNumber { get; set; }
    }
    public static class activeUser
    {
        public static string Id
        {
            get
            {
                return Preferences.Get("UserId", "");
            }
            set
            {
                Preferences.Set("UserId", value);
            }
        }
        public static string Email
        {
            get
            {
                return Preferences.Get("UserEmail", "");
            }
            set
            {
                Preferences.Set("UserEmail", value);
            }
        }
        public static string UserName
        {
            get
            {
                return Preferences.Get("UserName", "");
            }
            set
            {
                Preferences.Set("UserName", value);
            }
        }
        public static int PhoneNumber
        {
            get
            {
                return Preferences.Get("UserPhoneNumber", 0);
            }
            set
            {
                Preferences.Set("UserPhoneNumber", value);
            }
        }
        
    }
}
