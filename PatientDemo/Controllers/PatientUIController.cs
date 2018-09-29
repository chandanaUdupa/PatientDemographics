using Newtonsoft.Json;
using PatientDemo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PatientDemo.Controllers
{
    public class PatientUIController : Controller
    {
        private string baseURL = "http://localhost:58443/";

        // GET: PatientUI
        public ActionResult Index()
        {
            return View();
        }

        // GET: PatientUI/Details/5
        public async Task<ActionResult> Details(int id)
        {
            PatientRecord patient = new PatientRecord();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(baseURL);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                //Sending request to find web api REST service resource Get Patient using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("api/Patient/" + id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var patientResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Patient 
                    XmlSerializer serializer = new XmlSerializer(typeof(PatientRecord));
                    StringReader sr = new StringReader(patientResponse);
                    patient = (PatientRecord)serializer.Deserialize(new NamespaceIgnorantXmlTextReader(sr));
                }
                //returning the patient to view  
                return View(patient);
            }
        }

        // POST: PatientUI/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: PatientUI/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
