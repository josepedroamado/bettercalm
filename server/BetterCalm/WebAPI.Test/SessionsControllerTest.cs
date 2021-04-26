using BLInterfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Controllers;

namespace WebAPI.Test
{
	[TestClass]
	public class SessionsControllerTest
	{
		[TestMethod]
		public void LoginOk()
		{
			UserCredentialsModel credentialsParameters = new UserCredentialsModel()
			{
				EMail = "a@a.com",
				Password = "1234"
			};

			string expectedToken = "6813521C-A18F-493A-9DF7-BE6704DAA2CC";
			Mock<IUserManager> mock = new Mock<IUserManager>(MockBehavior.Strict);
			mock.Setup(m => m.Login(credentialsParameters.EMail, credentialsParameters.Password)).Returns(expectedToken);

			SessionsController controller = new SessionsController(mock.Object);

			IActionResult result = controller.Post(credentialsParameters);
			OkObjectResult objectResult = result as OkObjectResult;
			string obtainedToken = (objectResult.Value as SessionInfoModel).Token;

			mock.VerifyAll();
			Assert.AreEqual(expectedToken, obtainedToken);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidCredentialsException))]
		public void LoginFailedUserNotFound()
		{
			UserCredentialsModel credentialsParameters = new UserCredentialsModel()
			{
				EMail = "a@a.com",
				Password = "1234"
			};

			string expectedToken = "";
			Mock<IUserManager> mock = new Mock<IUserManager>(MockBehavior.Strict);
			mock.Setup(m => m.Login(credentialsParameters.EMail, credentialsParameters.Password)).Throws(new InvalidCredentialsException());

			SessionsController controller = new SessionsController(mock.Object);

			IActionResult result = controller.Post(credentialsParameters);
			OkObjectResult objectResult = result as OkObjectResult;
			string obtainedToken = (objectResult.Value as SessionInfoModel).Token;

			mock.VerifyAll();
			Assert.AreEqual(expectedToken, obtainedToken);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidCredentialsException))]
		public void LoginFailedWrongPassword()
		{
			UserCredentialsModel credentialsParameters = new UserCredentialsModel()
			{
				EMail = "a@a.com",
				Password = "1234"
			};

			string expectedToken = "";
			Mock<IUserManager> mock = new Mock<IUserManager>(MockBehavior.Strict);
			mock.Setup(m => m.Login(credentialsParameters.EMail, credentialsParameters.Password)).Throws(new InvalidCredentialsException());

			SessionsController controller = new SessionsController(mock.Object);

			IActionResult result = controller.Post(credentialsParameters);
			OkObjectResult objectResult = result as OkObjectResult;
			string obtainedToken = (objectResult.Value as SessionInfoModel).Token;

			mock.VerifyAll();
			Assert.AreEqual(expectedToken, obtainedToken);
		}
	}
}
