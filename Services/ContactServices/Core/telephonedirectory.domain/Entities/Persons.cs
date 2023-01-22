using telephonedirectory.domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telephonedirectory.domain.Entities
{
    /// <summary>
    /// Kişiler
    /// </summary>
    public class Persons:BaseEntity
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
    }
}
