//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebFastfood.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class GEOLOCATION
    {
        public int id_geolocation { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public System.DateTime datetime { get; set; }
        public int id_order { get; set; }
    
        public virtual TORDER TORDER { get; set; }
    }
}
