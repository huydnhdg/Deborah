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
    
    public partial class B_Voucher
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public Nullable<int> Amount { get; set; }
        public string Cate { get; set; }
        public Nullable<System.DateTime> Createdate { get; set; }
        public string Createby { get; set; }
        public Nullable<System.DateTime> Activedate { get; set; }
        public string PhoneActive { get; set; }
    }
}