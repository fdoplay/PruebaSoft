using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Soft
{
    public class quotes
    {
        public Int32 id { get; set; }
        public Int32 iddoctor { get; set; }
        public Int32 idpaciente { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public DateTime fechainicio { get; set; }
        public DateTime fechafin { get; set; }
        public string txticono { get; set; }
        public string txtcolor { get; set; }
        public int estado { get; set; }
        public string name_doctor { get; set; }
        public string name_paciente { get; set; }
    }
}
