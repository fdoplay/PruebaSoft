using Aplication.Soft.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AppSoft.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Pagina Principal";
            //GestionCrearTabla cld = new GestionCrearTabla();
            //cld.CreaTablas();
            //SaveData();
            return View();
        }

        private void SaveData()
        {
            List<Domain.Soft.specialties> espe = Get_Specialties();
            var spe = new Aplication.Soft.GestionSpecialties().Save(espe);
            List<Domain.Soft.doctors> doc = Get_Doctors();
            var doctor = new Aplication.Soft.GestionDoctors().Save(doc);
            List<Domain.Soft.patients> pacientes = Get_Patients();
            var pacien= new Aplication.Soft.GestionPatients().Save(pacientes);
        }

        private List<Domain.Soft.specialties> Get_Specialties()
        {
            List<Domain.Soft.specialties> result = new List<Domain.Soft.specialties>();
            WebRequest request = WebRequest.Create("http://pruebas.apimedic.personalsoft.net:8082/api/v1/specialties/");
            request.Method = "GET";
            string resultado = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                System.IO.Stream stream = response.GetResponseStream();

                using (System.IO.StreamReader rd = new System.IO.StreamReader(stream))
                {
                    resultado = rd.ReadToEnd();
                }
            }
            if (resultado != null && resultado != "")
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Domain.Soft.specialties>>(resultado);
            }
            return result;
        }
        private List<Domain.Soft.doctors> Get_Doctors()
        {
            List<Domain.Soft.doctors> result = new List<Domain.Soft.doctors>();
            WebRequest request = WebRequest.Create("http://pruebas.apimedic.personalsoft.net:8082/api/v1/doctors/");
            request.Method = "GET";
            string resultado = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                System.IO.Stream stream = response.GetResponseStream();

                using (System.IO.StreamReader rd = new System.IO.StreamReader(stream))
                {
                    resultado = rd.ReadToEnd();
                }
            }
            if (resultado != null && resultado != "")
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Domain.Soft.doctors>>(resultado);
            }
            return result;
        }
        private List<Domain.Soft.patients> Get_Patients()
        {
            List<Domain.Soft.patients> result = new List<Domain.Soft.patients>();
            WebRequest request = WebRequest.Create("http://pruebas.apimedic.personalsoft.net:8082/api/v1/patients/");
            request.Method = "GET";
            string resultado = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                System.IO.Stream stream = response.GetResponseStream();

                using (System.IO.StreamReader rd = new System.IO.StreamReader(stream))
                {
                    resultado = rd.ReadToEnd();
                }
            }
            if (resultado != null && resultado != "")
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Domain.Soft.patients>>(resultado);
            }
            return result;
        }
    }
}
