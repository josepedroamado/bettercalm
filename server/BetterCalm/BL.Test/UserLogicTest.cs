using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BL.Test
{
	[TestClass]
	public class UserLogicTest
	{
		[TestMethod]
		public void CreateUser_DataIsCorrect_Created()
		{
			Role role = new Role()
			{
				Id = 1,
				Name = "Administrator",
				Description = "Administrator test"
			};

			User user = new User()
			{
				Email = "a@a.com",
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
			userMock.Setup(m => m.Get(user.Email)).Returns(user);

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);
			roleMock.Setup(m => m.Get(role.Name)).Returns(role);

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);
			
			userLogic.CreateUser(user);
			User obtainedUser = userLogic.GetUser(user.Email);

			userMock.VerifyAll();
			roleMock.VerifyAll();

			Assert.AreEqual(user, obtainedUser);
		}

		[TestMethod]
		[ExpectedException(typeof(AlreadyExistsException))]
		public void CreateUser_ExistingUser_ExceptionThrown()
		{
			Role role = new Role()
			{
				Id = 1,
				Name = "Administrator",
				Description = "Administrator test"
			};

			User user = new User()
			{
				Email = "a@a.com",
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
			userMock.Setup(m => m.Get(user.Email)).Returns(user);

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);
			roleMock.Setup(m => m.Get(role.Name)).Returns(role);

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);

			userLogic.CreateUser(user);
			userMock.Setup(m => m.Get(user.Email)).Throws(new AlreadyExistsException(user.Email));
			userLogic.CreateUser(user);
			User obtainedUser = userLogic.GetUser(user.Email);

			userMock.VerifyAll();
			roleMock.VerifyAll();

			Assert.AreEqual(user, obtainedUser);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void CreateUser_RoleNotFound_ExceptionThrown()
		{
			Role role = new Role()
			{
				Id = 1,
				Name = "Administrator",
				Description = "Administrator test"
			};

			User user = new User()
			{
				Email = "a@a.com",
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
			userMock.Setup(m => m.Get(user.Email)).Returns(user);

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);
			roleMock.Setup(m => m.Get(role.Name)).Throws(new NotFoundException(role.Name));

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);

			userLogic.CreateUser(user);
			User obtainedUser = userLogic.GetUser(user.Email);

			userMock.VerifyAll();
			roleMock.VerifyAll();

			Assert.IsNull(obtainedUser);
		}

		[TestMethod]
		public void UpdateUser_DatIsValid_Updated()
		{

			User user = new User()
			{
				Email = "a@a.com",
				Id = 1,
				Password = "1234Test",
				Name = "test"
			};

			Mock<IUserRepository> userMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userMock.Setup(m => m.Update(user));
			userMock.Setup(m => m.Get(user.Email)).Returns(user);
			userMock.Setup(m => m.Get(user.Id)).Returns(user);

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);

			userLogic.UpdateUser(user);
			User obtainedUser = userLogic.GetUser(user.Email);

			userMock.VerifyAll();

			Assert.AreEqual(user, obtainedUser);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void UpdateUser_UserNotFound_ExceptionThrown()
		{

			User user = new User()
			{
				Email = "a@a.com",
				Id = 1,
				Password = "1234Test",
				Name = "test"
			};

			Mock<IUserRepository> userMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userMock.Setup(m => m.Update(user));
			userMock.Setup(m => m.Get(user.Email)).Throws(new NotFoundException(user.Email));
			userMock.Setup(m => m.Get(user.Id)).Throws(new NotFoundException(user.Id.ToString()));

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);

			userLogic.UpdateUser(user);
			User obtainedUser = userLogic.GetUser(user.Email);

			userMock.VerifyAll();

			Assert.IsNull(obtainedUser);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void Delete_UserExists_Delted()
		{
			string email = "a@a.com";
			int toDelete = 1;

			Mock<IUserRepository> userMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userMock.Setup(m => m.Delete(toDelete));
			userMock.Setup(m => m.Get(email)).Throws(new NotFoundException(email));

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);

			UserLogic userLogic = new UserLogic(userMock.Object, roleMock.Object);

			userLogic.DeleteUser(toDelete);
			User obtainedUser = userLogic.GetUser(email);

			userMock.VerifyAll();

			Assert.IsNull(obtainedUser);
		}

		[TestMethod]
		public void GetUsersByRole_RoleExists_UsersWithRoleFetched()
		{
			string roleName = "Administrator";

			User user = new User()
			{
				Email = "a@a.com",
				Id = 1,
				Password = "1234Test",
				Name = "test"
			};

			List<User> expectedUsers = new List<User>()
			{
				user
			};

			Mock<IRoleRepository> roleMock = new Mock<IRoleRepository>(MockBehavior.Strict);
			roleMock.Setup(m => m.GetUsers(roleName)).Returns(expectedUsers);

			Mock<IUserRepository> userMock = new Mock<IUserRepository>(MockBehavior.Strict);

			UserLogic logic = new UserLogic(userMock.Object, roleMock.Object);

			ICollection<User> obtainedUsers = logic.GetUsersByRole(roleName);

			CollectionAssert.AreEqual(expectedUsers, obtainedUsers.ToList());
		}
	}
}
