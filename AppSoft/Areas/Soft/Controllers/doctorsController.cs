using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppSoft.Areas.Soft.Controllers
{
    [RoutePrefix("api/doctors")]
    public class doctorsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Domain.Soft.doctors> Get()
        {
            return new Aplication.Soft.GestionDoctors().Get();
        }

        // GET api/<controller>/5
        public Domain.Soft.doctors Get(int id)
        {
            return new Aplication.Soft.GestionDoctors().GetId(id);
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