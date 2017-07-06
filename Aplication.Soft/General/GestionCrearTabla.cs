using Infraestructura.Soft.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Soft.General
{
    public class GestionCrearTabla
    {
        public void CreaTablas()
        {
            RepositoryCrearTabla cls = new RepositoryCrearTabla();
            cls.CreaTablas();
        }
    }
}
