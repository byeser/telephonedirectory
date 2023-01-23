using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact.application.Handlers.Reports.Dtos
{
    public class PersonsResponse
    {
        public string UUID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Firma { get; set; }
        public List<ContactInfoResponse> Contact { get; set; }=new List<ContactInfoResponse>(); 
    }
}
