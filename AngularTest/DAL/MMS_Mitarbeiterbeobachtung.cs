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
    
    public partial class MMS_Mitarbeiterbeobachtung
    {
        public int Beobachter_ID { get; set; }
        public System.DateTime Datum { get; set; }
        public int Mitarbeiter_ID { get; set; }
        public Nullable<int> Taetigkeit_ID { get; set; }
        public Nullable<int> Bereich_ID { get; set; }
        public Nullable<int> Ort_ID { get; set; }
        public Nullable<int> Funktion_ID { get; set; }
    
        public virtual MMS_Beobachter MMS_Beobachter { get; set; }
        public virtual MMS_Bereich MMS_Bereich { get; set; }
        public virtual MMS_Mitarbeiterfunktion MMS_Mitarbeiterfunktion { get; set; }
        public virtual MMS_Ort MMS_Ort { get; set; }
        public virtual MMS_Taetigkeit MMS_Taetigkeit { get; set; }
    }
}
