//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class purchaseOrder
    {
        public purchaseOrder()
        {
            this.purchaseOrderDetails = new HashSet<purchaseOrderDetail>();
        }
    
        public string purchaseOrderId { get; set; }
        public string supplierId { get; set; }
        public Nullable<System.DateTime> orderDate { get; set; }
        public Nullable<System.DateTime> deliveryDate { get; set; }
        public string status { get; set; }
    
        public virtual ICollection<purchaseOrderDetail> purchaseOrderDetails { get; set; }
        public virtual supplier supplier { get; set; }
    }
}