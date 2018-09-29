using Microsoft.VisualStudio.TestTools.UnitTesting;
using PatientDemo.BusinessObject.BusinessEntities;
using PatientDemo.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace PatientDemo.Controllers.Tests
{
    [TestClass()]
    public class PatientControllerTests
    {
        [TestMethod()]
        public void GetPatientTest()
        {
            PatientController controller = new PatientController();
            IHttpActionResult actionResult = controller.Get(3);
            var contentResult = actionResult as OkNegotiatedContentResult<PatientRecord>;
            Assert.IsNotNull(contentResult);
            var content = contentResult.Content;
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, content.Id);
        }
        [TestMethod()]
        public void PostPatientTest()
        {
            // Arrange
            var controller = new PatientController();
            PatientRecord patientRecord = new PatientRecord
            {
                Surname = "Test",
                Forenames = "Test",
                Gender = "Male",
                Date_of_Birth = DateTime.Now
            };

            // Act
            IHttpActionResult actionResult = controller.PostPatient(patientRecord);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<PatientRecord>;

            // Assert  
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.IsNotNull(createdResult.RouteValues["id"]);
        }

        [TestMethod()]
        public void PutPatientTest()
        {
            // Arrange  
            var controller = new PatientController();
            PatientRecord patientRecord = new PatientRecord
            {
                Surname = "Test",
                Forenames = "Test",
                Gender = "Male",
                Date_of_Birth = DateTime.Now
            };

            IHttpActionResult actionResult = controller.PutPatient(5, patientRecord);
            var contentResult = actionResult as NegotiatedContentResult<PatientRecord>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.Accepted, contentResult.StatusCode);
            Assert.IsNotNull(contentResult.Content);
        }

    }
}