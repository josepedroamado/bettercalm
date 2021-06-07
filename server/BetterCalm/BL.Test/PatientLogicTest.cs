using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Test
{
    [TestClass]
    public class PatientLogicTest
    {
        [TestMethod]
        public void GetAllWithoutDiscountAndRequiredAppointmentQuantity_PatientsExistAndMeetRequirements_Fetched()
        {
			IEnumerable<Patient> expectedPatients = GetAllWithoutDiscountExpectedPatientsAndRequiredAppointmentQuantity();
			Mock<IPatientRepository> patientRepoMock = new Mock<IPatientRepository>(MockBehavior.Strict);
            patientRepoMock.Setup(m => m.GetAllWithoutDiscount(It.IsAny<int>())).Returns(expectedPatients);
			PatientLogic patientLogic = new PatientLogic(patientRepoMock.Object);

			IEnumerable<Patient> obtainedPatients = patientLogic.GetAllWithoutDiscountAndRequiredAppointmentQuantity();

			Assert.IsTrue(expectedPatients.SequenceEqual(obtainedPatients));
		}

		private List<Patient> GetAllWithoutDiscountExpectedPatientsAndRequiredAppointmentQuantity()
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

		[TestMethod]
		[ExpectedException(typeof(CollectionEmptyException))]
		public void GetAllWithoutDiscountAndRequiredAppointmentQuantity_NoPatientsExist_ExceptionThrown()
		{
			IEnumerable<Patient> expectedPatients = GetAllWithoutDiscountExpectedPatientsAndRequiredAppointmentQuantity();
			Mock<IPatientRepository> patientRepoMock = new Mock<IPatientRepository>(MockBehavior.Strict);
			patientRepoMock.Setup(m => m.GetAllWithoutDiscount(It.IsAny<int>())).Throws(new CollectionEmptyException("Patients"));
			PatientLogic patientLogic = new PatientLogic(patientRepoMock.Object);

			IEnumerable<Patient> obtainedPatients = patientLogic.GetAllWithoutDiscountAndRequiredAppointmentQuantity();

			Assert.IsNull(obtainedPatients);
		}

		[TestMethod]
		[ExpectedException(typeof(NoPatientsMeetCriteriaException))]
		public void GetAllWithoutDiscountAndRequiredAppointmentQuantity_NoPatientsMeetCriteria_ExceptionThrown()
		{
			Mock<IPatientRepository> patientRepoMock = new Mock<IPatientRepository>(MockBehavior.Strict);
			patientRepoMock.Setup(m => m.GetAllWithoutDiscount(It.IsAny<int>())).Returns(new List<Patient>());
			PatientLogic patientLogic = new PatientLogic(patientRepoMock.Object);

			IEnumerable<Patient> obtainedPatients = patientLogic.GetAllWithoutDiscountAndRequiredAppointmentQuantity();

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

			Mock<IPatientRepository> patientRepoMock = new Mock<IPatientRepository>(MockBehavior.Strict);
			patientRepoMock.Setup(m => m.Get(patient.Email)).Returns(patient);
			PatientLogic patientLogic = new PatientLogic(patientRepoMock.Object);

			Patient obtainedPatient = patientLogic.Get(patient.Email);

			Assert.AreEqual(patient, obtainedPatient);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Get_PatientNotFound_ExceptionThrown()
		{
			Mock<IPatientRepository> patientRepoMock = new Mock<IPatientRepository>(MockBehavior.Strict);
			patientRepoMock.Setup(m => m.Get("notFoundEmail@fail.com")).Throws(new NotFoundException("Patient"));
			PatientLogic patientLogic = new PatientLogic(patientRepoMock.Object);

			Patient obtainedPatient = patientLogic.Get("notFoundEmail@fail.com");

			Assert.IsNull(obtainedPatient);
		}

		[TestMethod]
		public void Update_DataIsCorrect_Updated()
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
				AppointmentDiscount = new AppointmentDiscount() { Id = 1, Discount = 50}
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

			Mock<IPatientRepository> patientRepoMock = new Mock<IPatientRepository>(MockBehavior.Strict);
			patientRepoMock.Setup(m => m.Get(updated.Email)).Returns(original);
			patientRepoMock.Setup(m => m.Update(updated));
			PatientLogic patientLogic = new PatientLogic(patientRepoMock.Object);

			patientLogic.Update(updated);
			patientRepoMock.Setup(m => m.Get(updated.Email)).Returns(updated);
			Patient obtainedPatient = patientLogic.Get(updated.Email);

			Assert.AreEqual(updated, obtainedPatient);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Update_PatientNotFound_ExceptionThrown()
		{
			Patient updated = new Patient()
			{
				Id = 1,
				BirthDate = new DateTime(1950, 1, 1),
				Email = "john.doe@gmail.com",
				FirstName = "Arthur",
				LastName = "Morgan",
				Phone = "8885551234"
			};

			Mock<IPatientRepository> patientRepoMock = new Mock<IPatientRepository>(MockBehavior.Strict);
			patientRepoMock.Setup(m => m.Get(updated.Email)).Throws(new NotFoundException("Patient"));
			patientRepoMock.Setup(m => m.Update(updated));
			PatientLogic patientLogic = new PatientLogic(patientRepoMock.Object);

			patientLogic.Update(updated);
			Patient obtainedPatient = patientLogic.Get(updated.Email);

			patientRepoMock.VerifyAll();
			Assert.IsNull(obtainedPatient);
		}
	}
}
