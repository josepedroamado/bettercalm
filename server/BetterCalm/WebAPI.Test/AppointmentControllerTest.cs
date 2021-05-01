using BLInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using WebAPI.Controllers;

namespace WebAPI.Test
{
	[TestClass]
	public class AppointmentControllerTest
	{
		[TestMethod]
		public void PostOk()
		{
			AppointmentInputModel input = new AppointmentInputModel()
			{
				BirthDate = DateTime.Now,
				EMail = "a@a.com",
				IllnessId = 1,
				LastName = "aUser",
				Name = "aName",
				Phone = "09368574"
			};

			Appointment appointment = input.ToEntity();
			appointment.Psychologist = new Psychologist()
			{
				FirstName = "Jhon",
				LastName = "Doe",
				Format = Format.OnSite
			};
			appointment.Address = "appointment address";

			AppointmentOutputModel expectedOutput = new AppointmentOutputModel()
			{
				Address = appointment.Address,
				Format = appointment.Psychologist.Format.ToString(),
				PsychologistName = appointment.Psychologist.GetFullName(),
				Date = appointment.Date
			};

			Mock<IAppointmentLogic> logicMock = new Mock<IAppointmentLogic>(MockBehavior.Strict);
			logicMock.Setup(m => m.CreateAppointment(It.IsAny<Patient>(), It.IsAny<Illness>()))
				.Returns(appointment);

			AppointmentsController controller = new AppointmentsController(logicMock.Object);

			IActionResult result = controller.Post(input);
			OkObjectResult objectResult = result as OkObjectResult;
			AppointmentOutputModel obtainedOutput = objectResult.Value as AppointmentOutputModel;

			logicMock.VerifyAll();
			Assert.AreEqual(expectedOutput, obtainedOutput);
		}

		[TestMethod]
		[ExpectedException(typeof(CollectionEmptyException))]
		public void PostFailedNoPsychologists()
		{
			AppointmentInputModel input = new AppointmentInputModel()
			{
				BirthDate = DateTime.Now,
				EMail = "a@a.com",
				IllnessId = 1,
				LastName = "aUser",
				Name = "aName",
				Phone = "09368574"
			};

			Mock<IAppointmentLogic> logicMock = new Mock<IAppointmentLogic>(MockBehavior.Strict);
			logicMock.Setup(m => m.CreateAppointment(It.IsAny<Patient>(), It.IsAny<Illness>()))
				.Throws(new CollectionEmptyException("Psychologists"));

			AppointmentsController controller = new AppointmentsController(logicMock.Object);

			IActionResult result = controller.Post(input);
			OkObjectResult objectResult = result as OkObjectResult;
			AppointmentOutputModel obtainedOutput = objectResult.Value as AppointmentOutputModel;

			logicMock.VerifyAll();
			Assert.IsNull(obtainedOutput);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void PostNotFoundIllness()
		{
			AppointmentInputModel input = new AppointmentInputModel()
			{
				BirthDate = DateTime.Now,
				EMail = "a@a.com",
				IllnessId = 1589,
				LastName = "aUser",
				Name = "aName",
				Phone = "09368574"
			};

			Mock<IAppointmentLogic> logicMock = new Mock<IAppointmentLogic>(MockBehavior.Strict);
			logicMock.Setup(m => m.CreateAppointment(It.IsAny<Patient>(), It.IsAny<Illness>()))
				.Throws(new NotFoundException(input.IllnessId.ToString()));

			AppointmentsController controller = new AppointmentsController(logicMock.Object);

			IActionResult result = controller.Post(input);
			OkObjectResult objectResult = result as OkObjectResult;
			AppointmentOutputModel obtainedOutput = objectResult.Value as AppointmentOutputModel;

			logicMock.VerifyAll();
			Assert.IsNull(obtainedOutput);
		}
	}
}
