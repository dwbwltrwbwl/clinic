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
    
    public partial class doctors
    {
        public doctors()
        {
            this.receptions = new HashSet<receptions>();
        }
        public string CurrentPhoto
        {
            get
            {
                if (String.IsNullOrEmpty(image) || String.IsNullOrWhiteSpace(image))
                    return @"/Images/nofoto.jpg";
                else
                    return @"/Images/" + image;
            }
        }
        public string FullName
            => $"{first_name} {last_name} {middle_name}".Trim();
        public int id_doctor { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string middle_name { get; set; }
        public Nullable<int> id_specialization { get; set; }
        public string telephone { get; set; }
        public Nullable<int> id_role { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string image { get; set; }
    
        public virtual roles roles { get; set; }
        public virtual specializations specializations { get; set; }
        public virtual ICollection<receptions> receptions { get; set; }
    }
}
