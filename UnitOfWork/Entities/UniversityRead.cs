using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Entities
{
    public class UniversityRead
    {
        /* Bu Class veritabanında veri dizi halinde tutulmadığı için oluşturulmuştur. */
        public string country { get; set; }
        public string name { get; set; }
        public string alpha_two_code { get; set; }
        public bool state_province { get; set; }
        public string domains { get; set; }
        public string web_pages { get; set; }
    }
}
