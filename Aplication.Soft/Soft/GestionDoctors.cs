using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Soft
{
    public class GestionDoctors
    {
        public bool Save(List<Domain.Soft.doctors> doctor)
        {
            bool result = false;
            Infraestructura.Soft.Repository.RepositoryDoctors ss = new Infraestructura.Soft.Repository.RepositoryDoctors();
            foreach (var doc in doctor)
            {
                doc.idspecialties = doc.specialty_field.id;
                result = ss.Save(doc);
            }
            return result;
        }
        public bool Save(Domain.Soft.doctors doc)
        {
            bool result = false;
            Infraestructura.Soft.Repository.RepositoryDoctors ss = new Infraestructura.Soft.Repository.RepositoryDoctors();
            result = ss.Save(doc);
            return result;
        }
        public Domain.Soft.doctors GetId(Int32 id)
        {
            return new Infraestructura.Soft.Repository.RepositoryDoctors().GetId(id);
        }
        public List<Domain.Soft.doctors> Get()
        {
            return new Infraestructura.Soft.Repository.RepositoryDoctors().Get();
        }
    }
}
