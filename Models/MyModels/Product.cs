using System;
using System.Collections.Generic;
using System.Text;

namespace WebFastfood.Models.MyModels
{
    public class Product
    {
        public int id_product { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public decimal price { get; set; }
        public bool available { get; set; }
        public int id_admin { get; set; }
        public string picture { get; set; }
    }
}
