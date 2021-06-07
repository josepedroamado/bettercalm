using DataAccessInterfaces;
using Domain;
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
	}
}
