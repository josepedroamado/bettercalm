using BLInterfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
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

		[TestMethod]
		public void LogoutOk()
		{
			UserCredentialsModel credentialsParameters = new UserCredentialsModel()
			{
				EMail = "a@a.com",
				Password = "1234"
			};

			string expectedToken = "token1234";
			Mock<IUserManager> userManagerMock = new Mock<IUserManager>(MockBehavior.Strict);
			userManagerMock.Setup(m => m.Logout(expectedToken));
			userManagerMock.Setup(m => m.Login(credentialsParameters.EMail, credentialsParameters.Password)).Returns("newToken");

			SessionsController controller = new SessionsController(userManagerMock.Object);

			controller.Delete(expectedToken);
			IActionResult result = controller.Post(credentialsParameters);
			OkObjectResult objectResult = result as OkObjectResult;
			string obtainedToken = (objectResult.Value as SessionInfoModel).Token;

			userManagerMock.VerifyAll();
			Assert.AreNotEqual(expectedToken, obtainedToken);
		}
	}
}
