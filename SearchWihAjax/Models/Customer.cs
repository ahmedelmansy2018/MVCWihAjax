//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SearchWihAjax.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Customer
    {
        public Customer()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int Customerid { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
    }
}
