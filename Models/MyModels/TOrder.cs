using System;
using System.Collections.Generic;
using System.Text;

namespace WebFastfood.Models.MyModels
{
    public class TOrder
    {
        public int id_order { get; set; }
        public string order_state { get; set; }
        public Nullable<double> latitude { get; set; }
        public Nullable<double> longitude { get; set; }
        public Nullable<System.DateTime> order_datetime { get; set; }
        public string order_code { get; set; }
        public Nullable<byte> delivery_state { get; set; }
        public Nullable<System.DateTime> received_datetime { get; set; }
        public Nullable<int> id_delivery_man { get; set; }
        public int id_client { get; set; }
    }
}
