//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace clinic.ApplicationData
{
    using System;
    using System.Collections.Generic;
    
    public partial class patients
    {
        public patients()
        {
            this.receptions = new HashSet<receptions>();
        }
        public string FullName
            => $"{first_name} {last_name} {middle_name}".Trim();
        public int id_patient { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public Nullable<System.DateTime> date_of_birth { get; set; }
        public string gender { get; set; }
        public string policy_number { get; set; }
        public Nullable<int> id_site { get; set; }
        public string telephone { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public Nullable<int> id_role { get; set; }
    
        public virtual roles roles { get; set; }
        public virtual site site { get; set; }
        public virtual ICollection<receptions> receptions { get; set; }
    }
}
