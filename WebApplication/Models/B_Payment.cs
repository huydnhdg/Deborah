//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class B_Payment
    {
        public long Id { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public int PointCharge { get; set; }
        public string PayCate { get; set; }
        public Nullable<int> PayAmount { get; set; }
        public bool Status { get; set; }
        public int Point { get; set; }
        public string Phone { get; set; }
        public string PayContent { get; set; }
    }
}
