using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Globalization;

namespace AppSoft.Areas.Soft.Controllers
{
    [RoutePrefix("api/quotes")]
    public class quotesController : ApiController
    {
        // GET: api/quotes
        public IEnumerable<Domain.Soft.quotes> Get()
        {
            return new Aplication.Soft.GestionQuotes().Get();
        }

        // GET: api/quotes/5
        public Domain.Soft.quotes Get(int id)
        {
            return new Aplication.Soft.GestionQuotes().GetId(id);
        }
        // GET: api/quotes/2017-
        public IEnumerable<Domain.Soft.quotes> Get(DateTime? start, DateTime? end)
        {
            return new Aplication.Soft.GestionQuotes().Get(start, end);
        }
        // POST: api/quotes
        public bool Post(Domain.Soft.quotes cita)
        {
            return new Aplication.Soft.GestionQuotes().Save(cita);
        }

        // PUT: api/quotes/5
        public void Put(int id)
        {
        }

        // DELETE: api/quotes/5
        public bool Delete(int id)
        {
            return new Aplication.Soft.GestionQuotes().Delete(id);
        }
        // GET: api/quotes/Eliminar/5
        [Route("Eliminar")]
        public bool GetDelete(int id)
        {
            return new Aplication.Soft.GestionQuotes().Delete(id);
        }
    }
}
