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
    
    public partial class MMS_Bereich
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MMS_Bereich()
        {
            this.MMS_Beobachtertouren = new HashSet<MMS_Beobachtertouren>();
            this.MMS_Mitarbeiterbeobachtung = new HashSet<MMS_Mitarbeiterbeobachtung>();
            this.MMS_Ort = new HashSet<MMS_Ort>();
            this.MMS_Tourinformation = new HashSet<MMS_Tourinformation>();
        }
    
        public int ID { get; set; }
        public string Bereich { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MMS_Beobachtertouren> MMS_Beobachtertouren { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MMS_Mitarbeiterbeobachtung> MMS_Mitarbeiterbeobachtung { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MMS_Ort> MMS_Ort { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MMS_Tourinformation> MMS_Tourinformation { get; set; }
    }
}
