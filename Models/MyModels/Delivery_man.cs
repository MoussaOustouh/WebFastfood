using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFastfood.Models.MyModels
{
    public class Delivery_man
    {
        public int id_delivery_man { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string photo { get; set; }
        public string transport { get; set; }
        public string matricule { get; set; }
        public Nullable<double> latitude { get; set; }
        public Nullable<double> longitude { get; set; }
        public bool authorized { get; set; }
        public Nullable<bool> state { get; set; }
        public int id_admin { get; set; }
        public string confirmation_code { get; set; }


        public JObject toJObject()
        {
            return JObject.FromObject(this);
        }
    }
}
