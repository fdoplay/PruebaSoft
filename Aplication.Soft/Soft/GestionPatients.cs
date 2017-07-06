using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Soft
{
    public class GestionPatients
    {
        public bool Save(List<Domain.Soft.patients> paciente)
        {
            bool result = false;
            Infraestructura.Soft.Repository.RepositoryPatients ss = new Infraestructura.Soft.Repository.RepositoryPatients();
            foreach (var pat in paciente)
            {
                result = ss.Save(pat);
            }
            return result;
        }
        public bool Save(Domain.Soft.patients pat)
        {
            bool result = false;
            Infraestructura.Soft.Repository.RepositoryPatients ss = new Infraestructura.Soft.Repository.RepositoryPatients();
            result = ss.Save(pat);
            return result;
        }
        public Domain.Soft.patients GetId(Int32 id)
        {
            return new Infraestructura.Soft.Repository.RepositoryPatients().GetId(id);
        }
        public List<Domain.Soft.patients> Get()
        {
            return new Infraestructura.Soft.Repository.RepositoryPatients().Get();
        }
    }
}
