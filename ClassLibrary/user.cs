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
    
    public partial class user
    {
        public user()
        {
            this.discrepancies = new HashSet<discrepancy>();
            this.reorderItems = new HashSet<reorderItem>();
            this.requisitions = new HashSet<requisition>();
        }
    
        public string userId { get; set; }
        public string name { get; set; }
        public string phoneNo { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string departmentId { get; set; }
        public string roleId { get; set; }
        public string password { get; set; }
        public Nullable<System.DateTime> startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public string status { get; set; }
    
        public virtual department department { get; set; }
        public virtual ICollection<discrepancy> discrepancies { get; set; }
        public virtual ICollection<reorderItem> reorderItems { get; set; }
        public virtual ICollection<requisition> requisitions { get; set; }
        public virtual role role { get; set; }
    }
}