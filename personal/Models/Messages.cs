//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace personal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Messages
    {
        public int idmessage { get; set; }
        public Nullable<int> idUsersend { get; set; }
        public Nullable<int> idUserget { get; set; }
        public string message { get; set; }
        public Nullable<System.DateTime> Sendtime { get; set; }
        public string status { get; set; }
    
        public virtual Users Users { get; set; }
        public virtual Users Users1 { get; set; }
    }
}
