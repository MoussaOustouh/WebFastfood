using System;
using System.Collections.Generic;
using System.Text;

namespace WebFastfood.Models.MyModels
{
    public class Admin
    {
        public int id_admin { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string photo { get; set; }
        public bool authorized { get; set; }
        public string confirmation_code { get; set; }
    }
}
