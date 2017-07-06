using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AppSoft.Areas.Soft.Controllers
{
    //[Authorize]
    [RoutePrefix("api/patients")]
    public class patientsController : ApiController
    {
        // GET api/patients
        //[Route("patients")]
        public IEnumerable<Domain.Soft.patients> Get()
        {
            return new Aplication.Soft.GestionPatients().Get() ;
        }

        // GET api/patients/5
        //[Route("patients")]
        public Domain.Soft.patients Get(int id)
        {
            return new Aplication.Soft.GestionPatients().GetId(id);
        }

        //// POST api/patients
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