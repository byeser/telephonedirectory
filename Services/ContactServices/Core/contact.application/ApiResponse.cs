using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace contact.application
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            errors = new List<string>();
            success = new List<string>() { "İşlem başarılı" };
            code = 200;
        }
        public string version { get { return "1.0"; } }

        public int code { get; set; }
        public List<string> errors { get; set; }
        public List<string> success { get; set; }
        public T data { get; set; }
    }
}
