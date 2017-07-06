using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Soft
{
    public class patients
    {
        public Int32 id { get; set; }
        public string history { get; set; }
        public string identification { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string genre { get; set; }
        public string civil_status { get; set; }
        public string blood_type { get; set; }
        public DateTime date_birth { get; set; }
        public string city_birth { get; set; }
        public string url { get; set; }
    }
}
