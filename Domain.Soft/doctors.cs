using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Soft
{
    public class doctors
    {
        public Int32 id { get; set; }
        public string url { get; set; }
        public string identification { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string blood_type { get; set; }
        public Int32 idspecialties { get; set; }
        public specialties specialty_field { get; set; }
    }
}
