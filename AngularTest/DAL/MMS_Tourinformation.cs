//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AngularTest.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class MMS_Tourinformation
    {
        public int ID { get; set; }
        public Nullable<int> Bereich_ID { get; set; }
        public string Art { get; set; }
        public Nullable<int> Dauer { get; set; }
        public string Name { get; set; }
    
        public virtual MMS_Bereich MMS_Bereich { get; set; }
    }
}
