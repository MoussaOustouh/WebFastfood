using System;
using System.Collections.Generic;
using System.Text;

namespace WebFastfood.Models.MyModels
{
    public class Order_content
    {
        public int quantity { get; set; }
        public decimal price { get; set; }
        public int id_order { get; set; }
        public int id_product { get; set; }
        public string productTitle { get; set; }
    }
}
