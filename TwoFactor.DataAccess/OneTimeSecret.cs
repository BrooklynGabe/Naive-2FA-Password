//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TwoFactor.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class OneTimeSecret
    {
        public string UserLoginName { get; set; }
        public byte[] Secret { get; set; }
        public System.DateTime ValidUntil { get; set; }
    }
}
