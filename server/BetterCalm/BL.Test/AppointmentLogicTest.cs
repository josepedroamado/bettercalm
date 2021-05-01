using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Test
{
	[TestClass]
	public class AppointmentLogicTest
	{
		[TestMethod]
		public void CreateAppointmentOnSiteOk()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				EMail = "patient@gmail.com",
				FirstName = "Patient",
				LastName = "Perez",
				Id = 1,
				Phone = "091569874"
			};

			Illness illness = new Illness()
			{
				Id = 1,
				Name = "Depresion"
			};

			Psychologist psychologist = new Psychologist()
			{
				Id = 1,
				FirstName = "Juan",
				LastName = "Sartori",
				Address = "Calle 1234",
				Format = Format.OnSite,
				CreatedDate = DateTime.Today.AddMonths(-3)
			};



			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Returns(patient);

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(psychologist.Id)).Returns(psychologist);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsTrue(appointment.Address.Equals(psychologist.Address) &&
				appointment.Psychologist.Equals(psychologist));
		}

		[TestMethod]
		public void CreateAppointmentRemoteOk()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				EMail = "patient@gmail.com",
				FirstName = "Patient",
				LastName = "Perez",
				Id = 1,
				Phone = "091569874"
			};

			Illness illness = new Illness()
			{
				Id = 1,
				Name = "Depresion"
			};

			Psychologist psychologist = new Psychologist()
			{
				Id = 1,
				FirstName = "Juan",
				LastName = "Sartori",
				Address = "Calle 1234",
				Format = Format.Remote,
				CreatedDate = DateTime.Today.AddMonths(-3)
			};



			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Returns(patient);

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(psychologist.Id)).Returns(psychologist);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsTrue(!appointment.Address.Equals(psychologist.Address) &&
				appointment.Psychologist.Equals(psychologist));
		}

		[TestMethod]
		public void CreateAppointmentNewPatientOk()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				EMail = "patient@gmail.com",
				FirstName = "Patient",
				LastName = "Perez",
				Id = 1,
				Phone = "091569874"
			};

			Illness illness = new Illness()
			{
				Id = 1,
				Name = "Depresion"
			};

			Psychologist psychologist = new Psychologist()
			{
				Id = 1,
				FirstName = "Juan",
				LastName = "Sartori",
				Address = "Calle 1234",
				Format = Format.Remote,
				CreatedDate = DateTime.Today.AddMonths(-3)
			};



			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Throws(new NotFoundException(patient.EMail));

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(psychologist.Id)).Returns(psychologist);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsTrue(!appointment.Address.Equals(psychologist.Address) &&
				appointment.Psychologist.Equals(psychologist));
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void CreateAppointmentIllnessNotFound()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				EMail = "patient@gmail.com",
				FirstName = "Patient",
				LastName = "Perez",
				Id = 1,
				Phone = "091569874"
			};

			Illness illness = new Illness()
			{
				Id = 1,
				Name = "Depresion"
			};

			Psychologist psychologist = new Psychologist()
			{
				Id = 1,
				FirstName = "Juan",
				LastName = "Sartori",
				Address = "Calle 1234",
				Format = Format.Remote,
				CreatedDate = DateTime.Today.AddMonths(-3)
			};



			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Returns(patient);

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Throws(new NotFoundException(illness.Id.ToString()));

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(psychologist.Id)).Returns(psychologist);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}

		[TestMethod]
		[ExpectedException(typeof(CollectionEmptyException))]
		public void CreateAppointmentPsychologistsNotFound()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				EMail = "patient@gmail.com",
				FirstName = "Patient",
				LastName = "Perez",
				Id = 1,
				Phone = "091569874"
			};

			Illness illness = new Illness()
			{
				Id = 1,
				Name = "Depresion"
			};

			Psychologist psychologist = new Psychologist()
			{
				Id = 1,
				FirstName = "Juan",
				LastName = "Sartori",
				Address = "Calle 1234",
				Format = Format.Remote,
				CreatedDate = DateTime.Today.AddMonths(-3)
			};



			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Returns(patient);

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(psychologist.Id)).Throws(new CollectionEmptyException("Psychologists"));

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}
	}
}
