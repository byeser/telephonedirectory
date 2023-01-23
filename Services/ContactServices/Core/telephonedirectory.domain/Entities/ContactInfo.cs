using telephonedirectory.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telephonedirectory.domain.Entities
{
    /// <summary>
    /// İletişim Bilgileri
    /// </summary>
    public class ContactInfo:BaseEntity
    {
        public string PersonID { get; set; }
        public int InfoType { get; set; }
        public string InfoContent { get; set; }
        public DateTime Created { get; set; }
    }
}
