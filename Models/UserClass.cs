using System;
using System.Collections.Generic;
using System.Text;

namespace commerce.Models
{
    internal class UserClass
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int phone { get; set; }
        public DateTime creationTime { get; set; }
        public int NumberOfSales { get; set; }
        public string token { get; set; }

    }
}
