using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppSoft.Areas.Soft.Controllers
{
    [RoutePrefix("api/specialties")]
    public class specialtiesController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Domain.Soft.specialties> Get()
        {
            return new Aplication.Soft.GestionSpecialties().Get();
        }

        // GET api/<controller>/5
        public Domain.Soft.specialties Get(int id)
        {
            return new Aplication.Soft.GestionSpecialties().GetId(id);
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}