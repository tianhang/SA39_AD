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
    
    public partial class supplierPrice
    {
        public string supplierId { get; set; }
        public string itemId { get; set; }
        public Nullable<double> price { get; set; }
    
        public virtual item item { get; set; }
        public virtual supplier supplier { get; set; }
    }
}
