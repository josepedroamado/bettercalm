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
		public void Login_ValidCredentials_LoggedIn()
		{
			UserCredentialsModel credentialsParameters = new UserCredentialsModel()
			{
				EMail = "a@a.com",
				Password = "1234"
			};

			string expectedToken = "6813521C-A18F-493A-9DF7-BE6704DAA2CC";
			Mock<ISessionLogic> mock = new Mock<ISessionLogic>(MockBehavior.Strict);
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
		public void Login_UserNotFound_ExceptionThrown()
		{
			UserCredentialsModel credentialsParameters = new UserCredentialsModel()
			{
				EMail = "a@a.com",
				Password = "1234"
			};

			string expectedToken = "";
			Mock<ISessionLogic> mock = new Mock<ISessionLogic>(MockBehavior.Strict);
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
		public void Login_InvalidPassword_ExceptionThrown()
		{
			UserCredentialsModel credentialsParameters = new UserCredentialsModel()
			{
				EMail = "a@a.com",
				Password = "1234"
			};

			string expectedToken = "";
			Mock<ISessionLogic> mock = new Mock<ISessionLogic>(MockBehavior.Strict);
			mock.Setup(m => m.Login(credentialsParameters.EMail, credentialsParameters.Password)).Throws(new InvalidCredentialsException());

			SessionsController controller = new SessionsController(mock.Object);

			IActionResult result = controller.Post(credentialsParameters);
			OkObjectResult objectResult = result as OkObjectResult;
			string obtainedToken = (objectResult.Value as SessionInfoModel).Token;

			mock.VerifyAll();
			Assert.AreEqual(expectedToken, obtainedToken);
		}

		[TestMethod]
		public void Logout_ValidCredentialsSessionFound_LoggedOut()
		{
			UserCredentialsModel credentialsParameters = new UserCredentialsModel()
			{
				EMail = "a@a.com",
				Password = "1234"
			};

			string expectedToken = "token1234";
			Mock<ISessionLogic> sessionLogicMock = new Mock<ISessionLogic>(MockBehavior.Strict);
			sessionLogicMock.Setup(m => m.Logout(expectedToken));
			sessionLogicMock.Setup(m => m.Login(credentialsParameters.EMail, credentialsParameters.Password)).Returns("newToken");

			SessionsController controller = new SessionsController(sessionLogicMock.Object);

			controller.Delete(expectedToken);
			IActionResult result = controller.Post(credentialsParameters);
			OkObjectResult objectResult = result as OkObjectResult;
			string obtainedToken = (objectResult.Value as SessionInfoModel).Token;

			sessionLogicMock.VerifyAll();
			Assert.AreNotEqual(expectedToken, obtainedToken);
		}
	}
}
