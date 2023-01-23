using report.domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report.domain.Entities
{
    /// <summary>
    /// İletişim Bilgileri
    /// </summary>
    public class ContactInfo:BaseEntity
    {
        [ForeignKey("PersonID")]
        public string PersonID { get; set; }
        public int InfoType { get; set; }
        public string InfoContent { get; set; }
        public DateTime Created { get; set; }
    }
}
