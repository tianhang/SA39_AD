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
    
    public partial class menuByRole
    {
        public int menuByRoleId { get; set; }
        public Nullable<int> menuId { get; set; }
        public string roleId { get; set; }
    
        public virtual menu menu { get; set; }
    }
}
