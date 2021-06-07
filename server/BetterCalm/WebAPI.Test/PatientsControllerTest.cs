using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Controllers;

namespace WebAPI.Test
{
    [TestClass]
    public class PatientsControllerTest
    {
        [TestMethod]
        public void GetApproveDiscounts_PatientsExistAndMeetRequirements_Fetched()
        {
            IEnumerable<Patient> expectedPatients = GetApproveDiscountsExpectedPatients();
            Mock<IPatientLogic> mock = new Mock<IPatientLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetAllWithoutDiscountAndRequiredAppointmentQuantity()).Returns(expectedPatients);
            PatientsController controller = new PatientsController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<Patient> obtainedPatients = objectResult.Value as IEnumerable<Patient>;

            mock.VerifyAll();
            Assert.IsTrue(expectedPatients.SequenceEqual(obtainedPatients));
        }

        private List<Patient> GetApproveDiscountsExpectedPatients()
        {
            Patient johnDoe = new Patient()
            {
                BirthDate = new DateTime(1993, 11, 15),
                Email = "john.doe@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                Id = 1,
                Phone = "46465551256",
                AppointmentQuantity = 1
            };
            Patient janeDoe = new Patient()
            {
                BirthDate = new DateTime(1993, 11, 15),
                Email = "jane.doe@gmail.com",
                FirstName = "Jane",
                LastName = "Doe",
                Id = 2,
                Phone = "36325551478",
                AppointmentQuantity = 4
            };
            return new List<Patient>() { johnDoe, janeDoe };
        }
    }
}
