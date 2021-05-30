using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace BL.Test
{
    [TestClass]
	public class AppointmentLogicTest
	{
		[TestMethod]
		public void CreateAppointment_PatientExistsFormatOnSite_AppointmentCreated()
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};


			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Returns(patient);

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Returns(psychologist);
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsTrue(appointment.Address.Equals(psychologist.Address) &&
				appointment.Psychologist.Equals(psychologist));
		}

		[TestMethod]
		public void CreateAppointment_PatientExistsFormatRemote_AppointmentCreated()
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};


			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Returns(patient);

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Returns(psychologist);
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsTrue(!appointment.Address.Equals(psychologist.Address) &&
				appointment.Psychologist.Equals(psychologist));
		}

		[TestMethod]
		public void CreateAppointment_NewPatient_AppointmentCreated()
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};


			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Throws(new NotFoundException(patient.EMail));
			mockPatient.Setup(m => m.Add(patient));

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Returns(psychologist);
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsTrue(!appointment.Address.Equals(psychologist.Address) &&
				appointment.Psychologist.Equals(psychologist));
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void CreateAppointment_IllnessNotFound_ExceptionThrown()
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};


			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Returns(patient);

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Throws(new NotFoundException(illness.Id.ToString()));

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Returns(psychologist);
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}

		[TestMethod]
		[ExpectedException(typeof(CollectionEmptyException))]
		public void CreateAppointment_NoPsychologistsExist_ExceptionThrown()
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};


			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Returns(patient);
			mockPatient.Setup(m => m.Add(patient));

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Throws(new CollectionEmptyException("Psychologists"));
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}

		[TestMethod]
		[ExpectedException(typeof(CollectionEmptyException))]
		public void CreateAppointment_NoIllnessesExist_ExceptionThrown()
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};

			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Returns(patient);

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Throws(new CollectionEmptyException("Illnesses"));

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Throws(new CollectionEmptyException("Illnesses"));
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void CreateAppointment_NoPatientBirthDate_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};

			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Throws(new NotFoundException(patient.EMail));
			mockPatient.Setup(m => m.Add(patient)).Throws(new InvalidInputException("BirthDate is required"));

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Returns(psychologist);
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void CreateAppointment_NoPatientEMail_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};

			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Throws(new NotFoundException(patient.EMail));
			mockPatient.Setup(m => m.Add(patient)).Throws(new InvalidInputException("EMail is required"));

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Returns(psychologist);
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void CreateAppointment_NoPatientFirstName_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				EMail = "patient@gmail.com",
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};

			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Throws(new NotFoundException(patient.EMail));
			mockPatient.Setup(m => m.Add(patient)).Throws(new InvalidInputException("FirstName is required"));

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Returns(psychologist);
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void CreateAppointment_NoPatientLastName_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				EMail = "patient@gmail.com",
				FirstName = "Patient",
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};

			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Throws(new NotFoundException(patient.EMail));
			mockPatient.Setup(m => m.Add(patient)).Throws(new InvalidInputException("LastName is required"));

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Returns(psychologist);
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void CreateAppointment_NoPatientPhone_ExceptionThrown()
		{
			Patient patient = new Patient()
			{
				BirthDate = new DateTime(1993, 11, 15),
				EMail = "patient@gmail.com",
				FirstName = "Patient",
				LastName = "Perez",
				Id = 1
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};

			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Throws(new NotFoundException(patient.EMail));
			mockPatient.Setup(m => m.Add(patient)).Throws(new InvalidInputException("Phone is required"));

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Returns(psychologist);
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Returns(appointmentDuration);

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void CreateAppointment_InvalidDuration_ExceptionThrown()
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

			AppointmentDuration appointmentDuration = new AppointmentDuration(){
				Id = 1,
				Duration = new TimeSpan(1, 0, 0)
			};

			Mock<IPatientRepository> mockPatient = new Mock<IPatientRepository>(MockBehavior.Strict);
			mockPatient.Setup(m => m.Get(patient.EMail)).Returns(patient);

			Mock<IIllnessRepository> mockIllness = new Mock<IIllnessRepository>(MockBehavior.Strict);
			mockIllness.Setup(m => m.Get(illness.Id)).Returns(illness);

			Mock<IPsychologistRepository> mockPsychologist = new Mock<IPsychologistRepository>(MockBehavior.Strict);
			mockPsychologist.Setup(m => m.Get(illness, It.IsAny<DateTime>(), 5)).Returns(psychologist);
			mockPsychologist.Setup(m => m.Update(psychologist));

			Mock<IAppointmentDurationRepository> mockDuration = new Mock<IAppointmentDurationRepository>(MockBehavior.Strict);
			mockDuration.Setup(m => m.Get(appointmentDuration.Duration)).Throws(new NotFoundException(appointmentDuration.Duration.ToString()));

			AppointmentLogic appointmentLogic = new AppointmentLogic(mockPsychologist.Object, mockIllness.Object, mockPatient.Object, mockDuration.Object);
			Appointment appointment = appointmentLogic.CreateAppointment(patient, illness, appointmentDuration.Duration);

			mockIllness.VerifyAll();
			mockPatient.VerifyAll();
			mockPsychologist.VerifyAll();

			Assert.IsNull(appointment);
		}
	}
}
