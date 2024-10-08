//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TechCom.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Quotation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Quotation()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int QuoteID { get; set; }
        public Nullable<int> SupplierID { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> QuotationPrice { get; set; }
        public Nullable<int> QuotaQuantity { get; set; }
        public string OverAllDiscount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
