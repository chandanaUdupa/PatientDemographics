using PatientDemo.BusinessObject;
using PatientDemo.BusinessObject.BusinessEntities;
using PatientDemo.BusinessObject.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace PatientDemo.Controllers
{
    public class PatientController : ApiController
    {
        PatientManager patientManager = new PatientManager();
        // GET: api/Patient/1
        public IHttpActionResult Get(int id)
        {
            PatientRecord patient = patientManager.GetPatientDetails(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }


        // POST: api/Patient
        [ResponseType(typeof(PatientRecord))]
        public IHttpActionResult PostPatient(PatientRecord patient)
        {
            int patientId = patientManager.SetPatientDetails(patient);
            patient.Id = patientId;
            return CreatedAtRoute("DefaultApi", new { id = patientId }, patient);
        }

        // PUT: api/Patient/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatient(int id, PatientRecord patient)
        {
            int success = patientManager.SavePatientCDetails(id, patient);
            return Content(HttpStatusCode.Accepted, patient);
        }

    }
}