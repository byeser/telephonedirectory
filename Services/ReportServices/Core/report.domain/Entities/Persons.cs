using report.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report.domain.Entities
{
    /// <summary>
    /// Kişiler
    /// </summary>
    public class Persons:BaseEntity
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; } 
        public virtual ICollection<ContactInfo> ContactInfo { get; set; }   
    }
}
