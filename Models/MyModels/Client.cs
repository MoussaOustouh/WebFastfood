using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebFastfood.Models.MyModels
{
    public class Client
    {
        public int id_client { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string photo { get; set; }
        public string confirmation_code { get; set; }


        public JObject toJObject()
        {
            return JObject.FromObject(this);
        }
    }
}
