using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace report.application.Handlers.Reports.Dtos
{
    public class ContactInfoResponse
    {
        public string UUID { get; set; }
        public string PersonID { get; set; }
        public int InfoType { get; set; }
        public string InfoContent { get; set; }
        public DateTime Created { get; set; }
    }
}
