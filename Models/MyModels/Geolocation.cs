using System;
using System.Collections.Generic;
using System.Text;

namespace WebFastfood.Models.MyModels
{
    public class Geolocation
    {
        public int id_geolocation { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public System.DateTime datetime { get; set; }
        public int id_order { get; set; }
    }
}
