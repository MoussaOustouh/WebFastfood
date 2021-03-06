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
    
    public partial class TORDER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TORDER()
        {
            this.GEOLOCATIONs = new HashSet<GEOLOCATION>();
            this.ORDER_CONTENT = new HashSet<ORDER_CONTENT>();
        }
    
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
    
        public virtual CLIENT CLIENT { get; set; }
        public virtual DELIVERY_MAN DELIVERY_MAN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GEOLOCATION> GEOLOCATIONs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_CONTENT> ORDER_CONTENT { get; set; }
    }
}
