//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RTRS_S1s5
{
    using System;
    using System.Collections.Generic;
    
    public partial class NIC_Table
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NIC_Table()
        {
            this.Computer = new HashSet<Computer>();
        }
    
        public int id { get; set; }
        public string NIC_Model { get; set; }
        public string NICNum { get; set; }
        public string Built_in { get; set; }
        public Nullable<int> ComputerID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Computer> Computer { get; set; }
        public virtual Computer Computer1 { get; set; }
    }
}
