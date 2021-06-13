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
		public void Post_DataIsCorrect_Created()
		{
			AppointmentInputModel input = new AppointmentInputModel()
			{
				BirthDate = DateTime.Now,
				Email = "a@a.com",
				IllnessId = 1,
				LastName = "aUser",
				Name = "aName",
				Phone = "09368574",
				Duration = "01:00:00"
			};

			Appointment appointment = input.ToEntity();
			appointment.Psychologist = new Psychologist()
			{
				FirstName = "Jhon",
				LastName = "Doe",
				Format = Format.OnSite,
				Rate = new PsychologistRate()
                {
					Id = 1,
					HourlyRate = 1000
                }
			};
			appointment.Address = "appointment address";
			appointment.Discount = new AppointmentDiscount()
			{
				Id = 1,
				Discount = 50
			};
			appointment.TotalCost = 500;

			AppointmentOutputModel expectedOutput = new AppointmentOutputModel()
			{
				Address = appointment.Address,
				Format = appointment.Psychologist.Format.ToString(),
				PsychologistName = appointment.Psychologist.GetFullName(),
				Date = appointment.Date,
				Cost = 500,
				Discount = 50
			};

			Mock<IAppointmentLogic> logicMock = new Mock<IAppointmentLogic>(MockBehavior.Strict);
			logicMock.Setup(m => m.CreateAppointment(It.IsAny<Appointment>()))
				.Returns(appointment);

			AppointmentsController controller = new AppointmentsController(logicMock.Object);

			IActionResult result = controller.Post(input);
			OkObjectResult objectResult = result as OkObjectResult;
			AppointmentOutputModel obtainedOutput = objectResult.Value as AppointmentOutputModel;

			logicMock.VerifyAll();
			Assert.IsTrue((new AppointmentOutputModelComparer()).Compare(obtainedOutput, expectedOutput) == 0);
		}

		[TestMethod]
		[ExpectedException(typeof(CollectionEmptyException))]
		public void Post_NoPsychologistsEntered_ExceptionThrown()
		{
			AppointmentInputModel input = new AppointmentInputModel()
			{
				BirthDate = DateTime.Now,
				Email = "a@a.com",
				IllnessId = 1,
				LastName = "aUser",
				Name = "aName",
				Phone = "09368574",
				Duration = "01:00:00"
			};

			Mock<IAppointmentLogic> logicMock = new Mock<IAppointmentLogic>(MockBehavior.Strict);
			logicMock.Setup(m => m.CreateAppointment(It.IsAny<Appointment>()))
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
		public void Post_IllnessNotFound_ExceptionThrown()
		{
			AppointmentInputModel input = new AppointmentInputModel()
			{
				BirthDate = DateTime.Now,
				Email = "a@a.com",
				IllnessId = 1589,
				LastName = "aUser",
				Name = "aName",
				Phone = "09368574",
				Duration = "01:00:00"
			};

			Mock<IAppointmentLogic> logicMock = new Mock<IAppointmentLogic>(MockBehavior.Strict);
			logicMock.Setup(m => m.CreateAppointment(It.IsAny<Appointment>()))
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
