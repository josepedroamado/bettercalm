using BLInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
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
            IEnumerable<PatientModel> expectedPatientModels = GetApproveDiscountsExpectedPatientModels();
            IEnumerable<Patient> expectedPatients = expectedPatientModels.Select(patient => patient.ToEntity());
            Mock<IPatientLogic> mock = new Mock<IPatientLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetAllWithoutDiscountAndRequiredAppointmentQuantity()).Returns(expectedPatients);
            PatientsController controller = new PatientsController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<PatientModel> obtainedPatients = objectResult.Value as IEnumerable<PatientModel>;

            mock.VerifyAll();
            Assert.IsTrue(expectedPatientModels.SequenceEqual(obtainedPatients));
        }

        private List<PatientModel> GetApproveDiscountsExpectedPatientModels()
        {
            PatientModel johnDoe = new PatientModel()
            {
                BirthDate = new DateTime(1993, 11, 15),
                Email = "john.doe@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                Id = 1,
                Phone = "46465551256",
                AppointmentQuantity = 1
            };
            PatientModel janeDoe = new PatientModel()
            {
                BirthDate = new DateTime(1993, 11, 15),
                Email = "jane.doe@gmail.com",
                FirstName = "Jane",
                LastName = "Doe",
                Id = 2,
                Phone = "36325551478",
                AppointmentQuantity = 4
            };
            return new List<PatientModel>() { johnDoe, janeDoe };
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void GetApproveDiscounts_NoPatientsExist_ExceptionThrown()
        {         
            IEnumerable<PatientModel> expectedPatients = GetApproveDiscountsExpectedPatientModels();
            Mock<IPatientLogic> mock = new Mock<IPatientLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetAllWithoutDiscountAndRequiredAppointmentQuantity()).Throws(new CollectionEmptyException("Patients"));
            PatientsController controller = new PatientsController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<PatientModel> obtainedPatients = objectResult.Value as IEnumerable<PatientModel>;

            mock.VerifyAll();
            Assert.IsNull(obtainedPatients);
        }

        [TestMethod]
        [ExpectedException(typeof(NoPatientsMeetCriteriaException))]
        public void GetApproveDiscounts_NoPatientsMeetCriteria_ExceptionThrown()
        {
            IEnumerable<PatientModel> expectedPatients = GetApproveDiscountsExpectedPatientModels();
            Mock<IPatientLogic> mock = new Mock<IPatientLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetAllWithoutDiscountAndRequiredAppointmentQuantity()).Throws(new NoPatientsMeetCriteriaException());
            PatientsController controller = new PatientsController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<PatientModel> obtainedPatients = objectResult.Value as IEnumerable<PatientModel>;

            mock.VerifyAll();
            Assert.IsNull(obtainedPatients);
        }

        [TestMethod]
        public void Get_PatientFound_Fetched()
        {
            Patient patient = new Patient()
            {
                BirthDate = new DateTime(1993, 11, 15),
                Email = "patient@gmail.com",
                FirstName = "Patient",
                LastName = "Perez",
                Id = 1,
                Phone = "091569874"
            };
            Mock<IPatientLogic> mock = new Mock<IPatientLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Get(patient.Email)).Returns(patient);
            PatientsController controller = new PatientsController(mock.Object);

            IActionResult result = controller.Get(patient.Email);
            OkObjectResult objectResult = result as OkObjectResult;
            PatientModel obtainedPatient = (objectResult.Value as PatientModel);

            mock.VerifyAll();
            Assert.AreEqual(new PatientModel(patient), obtainedPatient);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Get_PatientNotFound_ExceptionThrown()
        {
            Mock<IPatientLogic> mock = new Mock<IPatientLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Get("notFoundEmail@fail.com")).Throws(new NotFoundException("Patient"));
            PatientsController controller = new PatientsController(mock.Object);

            IActionResult result = controller.Get("notFoundEmail@fail.com");
            OkObjectResult objectResult = result as OkObjectResult;
            PatientModel obtainedPatient = (objectResult.Value as PatientModel);

            mock.VerifyAll();
            Assert.IsNull(obtainedPatient);
        }

        [TestMethod]
        public void Patch_DataIsCorrect_Updated()
        {
            Patient original = new Patient()
            {
                BirthDate = new DateTime(1993, 11, 15),
                Email = "john.doe@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                Id = 1,
                Phone = "46465551256",
                AppointmentQuantity = 5,
                AppointmentDiscount = new AppointmentDiscount() { Id = 1, Discount = 50 }
            };

            Patient updated = new Patient()
            {
                BirthDate = new DateTime(1993, 11, 15),
                Email = "john.doe@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                Id = 1,
                Phone = "46465551256",
                AppointmentQuantity = 0,
                AppointmentDiscount = null
            };
            Mock<IPatientLogic> mock = new Mock<IPatientLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Update(updated));
            mock.Setup(m => m.Get(updated.Email)).Returns(original);
            PatientsController controller = new PatientsController(mock.Object);

            controller.Patch(updated);

            mock.Setup(m => m.Get(updated.Email)).Returns(updated);
            IActionResult result = controller.Get(updated.Email);
            OkObjectResult objectResult = result as OkObjectResult;
            PatientModel obtainedPatient = (objectResult.Value as PatientModel);

            mock.VerifyAll();
            Assert.AreEqual(new PatientModel(updated), obtainedPatient);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Update_PatientNotFound_ExceptionThrown()
        {
            Patient updated = new Patient()
            {
                BirthDate = new DateTime(1993, 11, 15),
                Email = "john.doe@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                Id = 1,
                Phone = "46465551256",
                AppointmentQuantity = 0,
                AppointmentDiscount = null
            };
            Mock<IPatientLogic> mock = new Mock<IPatientLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Update(updated));
            mock.Setup(m => m.Get(updated.Email)).Throws(new NotFoundException("Patient"));
            PatientsController controller = new PatientsController(mock.Object);

            controller.Patch(updated);

            IActionResult result = controller.Get(updated.Email);
            OkObjectResult objectResult = result as OkObjectResult;
            PatientModel obtainedPatient = (objectResult.Value as PatientModel);

            mock.VerifyAll();
            Assert.IsNull(obtainedPatient);
        }
    }
}
