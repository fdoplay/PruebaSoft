using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Soft
{
    public class GestionSpecialties
    {
        public bool Save(List<Domain.Soft.specialties> spe)
        {
            bool result = false;
            Infraestructura.Soft.Repository.RepositorySpecialties ss = new Infraestructura.Soft.Repository.RepositorySpecialties();
            foreach (var es in spe)
            {
                result = ss.Save(es);
            }
            return result;
        }
        public bool Save(Domain.Soft.specialties spe)
        {
            bool result = false;
            Infraestructura.Soft.Repository.RepositorySpecialties ss = new Infraestructura.Soft.Repository.RepositorySpecialties();
            result = ss.Save(spe);
            return result;
        }
        public Domain.Soft.specialties GetId(Int32 id)
        {
            return new Infraestructura.Soft.Repository.RepositorySpecialties().GetId(id);
        }
        public List<Domain.Soft.specialties> Get()
        {
            return new Infraestructura.Soft.Repository.RepositorySpecialties().Get();
        }
    }
}
