using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Soft
{
    public class GestionQuotes
    {
        public bool Save(List<Domain.Soft.quotes> citas)
        {
            bool result = false;
            Infraestructura.Soft.Repository.RepositoryQuotes ss = new Infraestructura.Soft.Repository.RepositoryQuotes();
            foreach (var cit in citas)
            {
                if (cit.id == 0)
                {
                    if (!ss.ExisteDoctor(cit))
                    {
                        result = ss.Save(cit);
                    }
                }
                else
                {
                    result = ss.Save(cit);
                }
            }
            return result;
        }
        public bool Save(Domain.Soft.quotes cita)
        {
            bool result = false;
            Infraestructura.Soft.Repository.RepositoryQuotes ss = new Infraestructura.Soft.Repository.RepositoryQuotes();
            if (cita.id == 0)   
            {
                if (!ss.ExisteDoctor(cita))
                {
                    result = ss.Save(cita);
                }
            }
            else
            {
                result = ss.Save(cita);
            }
            return result;
        }
        public Domain.Soft.quotes GetId(Int32 id)
        {
            return new Infraestructura.Soft.Repository.RepositoryQuotes().GetId(id);
        }
        public List<Domain.Soft.quotes> Get()
        {
            return new Infraestructura.Soft.Repository.RepositoryQuotes().Get();
        }
        public List<Domain.Soft.quotes> Get(DateTime? start, DateTime? end)
        {
            if (start != null)
            {
                start = new DateTime(start.Value.Year, start.Value.Month, 1);
            }
            else
            {
                start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            return new Infraestructura.Soft.Repository.RepositoryQuotes().Get(start, end);
        }
        public bool Delete(Int32 id)
        {
            return new Infraestructura.Soft.Repository.RepositoryQuotes().Delete(id);
        }
    }
}
