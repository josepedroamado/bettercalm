using BLInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System.Collections.Generic;
using WebAPI.Controllers;

namespace WebAPI.Test
{
	[TestClass]
	public class AdministratorControllerTest
	{
		[TestMethod]
		public void Post_DataIsCorrect_Created()
		{
			AdministratorInputModel input = new AdministratorInputModel()
			{
				EMail = "test@test.com",
				Name = "test",
				Password = "test1234"
			};

			Mock<IUserLogic> userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
			userLogicMock.Setup(m => m.CreateUser(It.IsAny<User>()));

			AdministratorsController controller = new AdministratorsController(userLogicMock.Object);

			IActionResult result = controller.Post(input);
			OkResult objectResult = result as OkResult;
			Assert.IsTrue(objectResult.StatusCode == 200);
		}

		[TestMethod]
		[ExpectedException(typeof(AlreadyExistsException))]
		public void Post_AlreadyExists_ExceptionThrown()
		{
			AdministratorInputModel input = new AdministratorInputModel()
			{
				EMail = "test@test.com",
				Name = "test",
				Password = "test1234"
			};

			Mock<IUserLogic> userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
			userLogicMock.Setup(m => m.CreateUser(It.IsAny<User>())).Throws(new AlreadyExistsException(input.EMail));

			AdministratorsController controller = new AdministratorsController(userLogicMock.Object);

			IActionResult result = controller.Post(input);
			ObjectResult objectResult = result as ObjectResult;
			Assert.IsTrue(objectResult.StatusCode == 400);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Post_RoleNotFound_ExceptionThrown()
		{
			AdministratorInputModel input = new AdministratorInputModel()
			{
				EMail = "test@test.com",
				Name = "test",
				Password = "test1234"
			};

			Mock<IUserLogic> userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
			userLogicMock.Setup(m => m.CreateUser(It.IsAny<User>())).Throws(new NotFoundException("Patient"));

			AdministratorsController controller = new AdministratorsController(userLogicMock.Object);

			IActionResult result = controller.Post(input);
			ObjectResult objectResult = result as ObjectResult;
			Assert.IsTrue(objectResult.StatusCode == 400);
		}

		[TestMethod]
		public void Patch_DataIsCorrect_Updated()
		{
			AdministratorInputModel input = new AdministratorInputModel()
			{
				Id = 1,
				EMail = "test@test.com",
				Name = "test",
				Password = "test1234"
			};

			Mock<IUserLogic> userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
			userLogicMock.Setup(m => m.UpdateUser(It.IsAny<User>()));

			AdministratorsController controller = new AdministratorsController(userLogicMock.Object);

			IActionResult result = controller.Patch(input);
			StatusCodeResult statusCodeResult = result as StatusCodeResult;
			Assert.IsTrue(statusCodeResult.StatusCode == 204);
		}

		[TestMethod]
		[ExpectedException(typeof(AlreadyExistsException))]
		public void Patch_EmailAlreadyExists_ExceptionThrown()
		{
			AdministratorInputModel input = new AdministratorInputModel()
			{
				Id = 1,
				EMail = "test@test.com",
				Name = "test",
				Password = "test1234"
			};

			Mock<IUserLogic> userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
			userLogicMock.Setup(m => m.UpdateUser(It.IsAny<User>())).Throws(new AlreadyExistsException(input.EMail));

			AdministratorsController controller = new AdministratorsController(userLogicMock.Object);

			IActionResult result = controller.Patch(input);
			ObjectResult objectResult = result as ObjectResult;
			Assert.IsTrue(objectResult.StatusCode == 400);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Patch_RoleNotFound_ExceptionThrown()
		{
			AdministratorInputModel input = new AdministratorInputModel()
			{
				Id = 1,
				EMail = "test@test.com",
				Name = "test",
				Password = "test1234"
			};

			Mock<IUserLogic> userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
			userLogicMock.Setup(m => m.UpdateUser(It.IsAny<User>())).Throws(new NotFoundException("Patient"));

			AdministratorsController controller = new AdministratorsController(userLogicMock.Object);

			IActionResult result = controller.Patch(input);
			ObjectResult objectResult = result as ObjectResult;
			Assert.IsTrue(objectResult.StatusCode == 400);
		}

		[TestMethod]
		public void Delete_UserFound_Deleted()
		{
			int id = 1;
			Mock<IUserLogic> userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
			userLogicMock.Setup(m => m.DeleteUser(id));

			AdministratorsController controller = new AdministratorsController(userLogicMock.Object);

			IActionResult result = controller.Delete(id);
			StatusCodeResult statusCodeResult = result as StatusCodeResult;
			Assert.IsTrue(statusCodeResult.StatusCode == 204);
		}

		[TestMethod]
		public void Get_UserFound_Fetched()
		{
			string roleName = "Administrator";
			User user = new User()
			{
				EMail = "a@a.com",
				Id = 1,
				Password = "1234Test",
				Name = "test"
			};

			List<User> expectedUsers = new List<User>()
			{
				user
			};

			Mock<IUserLogic> userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
			userLogicMock.Setup(m => m.GetUsersByRole(roleName)).Returns(expectedUsers);

			AdministratorsController controller = new AdministratorsController(userLogicMock.Object);

			IActionResult result = controller.Get();
			OkObjectResult okObjectResult = result as OkObjectResult;

			Assert.IsTrue(okObjectResult.StatusCode == 200);
		}
	}
}
