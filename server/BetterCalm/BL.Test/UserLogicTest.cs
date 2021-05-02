using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BL.Test
{
	[TestClass]
	public class UserLogicTest
	{
		[TestMethod]
		public void CreateUserOk()
		{
			Role role = new Role()
			{
				Id = 1,
				Name = "Administrator",
				Description = "Administrator test"
			};

			User user = new User()
			{
				EMail = "a@a.com",
				Id = 1,
				Password = "1234Test",
				Name = "test",
				Roles = new List<Role>()
				{
					role
				}
			};

			Mock<IUserRepository> userMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userMock.Setup(m => m.Add(user));
			userMock.Setup(m => m.Get(user.EMail)).Returns(user);

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);
			roleMock.Setup(m => m.Get(role.Name)).Returns(role);

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);
			
			userLogic.CreateUser(user);
			User obtainedUser = userLogic.GetUser(user.EMail);

			userMock.VerifyAll();
			roleMock.VerifyAll();

			Assert.AreEqual(user, obtainedUser);
		}

		[TestMethod]
		[ExpectedException(typeof(AlreadyExistsException))]
		public void CreateUserAlreadyExists()
		{
			Role role = new Role()
			{
				Id = 1,
				Name = "Administrator",
				Description = "Administrator test"
			};

			User user = new User()
			{
				EMail = "a@a.com",
				Id = 1,
				Password = "1234Test",
				Name = "test",
				Roles = new List<Role>()
				{
					role
				}
			};

			Mock<IUserRepository> userMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userMock.Setup(m => m.Add(user));
			userMock.Setup(m => m.Get(user.EMail)).Returns(user);

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);
			roleMock.Setup(m => m.Get(role.Name)).Returns(role);

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);

			userLogic.CreateUser(user);
			userMock.Setup(m => m.Get(user.EMail)).Throws(new AlreadyExistsException(user.EMail));
			userLogic.CreateUser(user);
			User obtainedUser = userLogic.GetUser(user.EMail);

			userMock.VerifyAll();
			roleMock.VerifyAll();

			Assert.AreEqual(user, obtainedUser);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void CreateRoleNotFound()
		{
			Role role = new Role()
			{
				Id = 1,
				Name = "Administrator",
				Description = "Administrator test"
			};

			User user = new User()
			{
				EMail = "a@a.com",
				Id = 1,
				Password = "1234Test",
				Name = "test",
				Roles = new List<Role>()
				{
					role
				}
			};

			Mock<IUserRepository> userMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userMock.Setup(m => m.Add(user));
			userMock.Setup(m => m.Get(user.EMail)).Returns(user);

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);
			roleMock.Setup(m => m.Get(role.Name)).Throws(new NotFoundException(role.Name));

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);

			userLogic.CreateUser(user);
			User obtainedUser = userLogic.GetUser(user.EMail);

			userMock.VerifyAll();
			roleMock.VerifyAll();

			Assert.IsNull(obtainedUser);
		}

		[TestMethod]
		public void UpdateUserOk()
		{

			User user = new User()
			{
				EMail = "a@a.com",
				Id = 1,
				Password = "1234Test",
				Name = "test"
			};

			Mock<IUserRepository> userMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userMock.Setup(m => m.Update(user));
			userMock.Setup(m => m.Get(user.EMail)).Returns(user);

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);

			userLogic.UpdateUser(user);
			User obtainedUser = userLogic.GetUser(user.EMail);

			userMock.VerifyAll();

			Assert.AreEqual(user, obtainedUser);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void UpdateUserNotFound()
		{

			User user = new User()
			{
				EMail = "a@a.com",
				Id = 1,
				Password = "1234Test",
				Name = "test"
			};

			Mock<IUserRepository> userMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userMock.Setup(m => m.Update(user));
			userMock.Setup(m => m.Get(user.EMail)).Throws(new NotFoundException(user.EMail));

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);

			userLogic.UpdateUser(user);
			User obtainedUser = userLogic.GetUser(user.EMail);

			userMock.VerifyAll();

			Assert.IsNull(obtainedUser);
		}
	}
}
