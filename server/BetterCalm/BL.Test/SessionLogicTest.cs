using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BL.Test
{
	[TestClass]
	public class SessionLogicTest
	{

		[TestMethod]
		public void Login_ValidCredentialsExistingSession_LoggedIn()
		{
			string expectedToken = "token1234";
			string queryEmail = "a@a.com";
			string queryPassword = "1234";

			User expectedUser = new User()
			{
				Email = queryEmail,
				Id = 1,
				Password = queryPassword,
				Name = "test"
			};

			Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userRepositoryMock.Setup(m => m.Get(queryEmail)).Returns(expectedUser);

			Session expectedSession = new Session()
			{
				Id = 1,
				Token = expectedToken,
				User = expectedUser
			};

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByEmail(queryEmail)).Returns(expectedSession);

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, userRepositoryMock.Object);

			string obtainedToken = sessionLogic.Login(queryEmail, queryPassword);
			Assert.AreEqual(expectedToken, obtainedToken);

		}

		[TestMethod]
		public void Login_ValidCredentialsNoSession_SessionCreated()
		{
			string queryEmail = "a@a.com";
			string queryPassword = "1234";

			User expectedUser = new User()
			{
				Email = queryEmail,
				Id = 1,
				Password = queryPassword,
				Name = "test"
			};

			Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userRepositoryMock.Setup(m => m.Get(queryEmail)).Returns(expectedUser);

			Session expectedSession = null;

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByEmail(queryEmail)).Returns(expectedSession);
			sessionRepositoryMock.Setup(m => m.Add(It.IsAny<Session>()));

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, userRepositoryMock.Object);

			string obtainedToken = sessionLogic.Login(queryEmail, queryPassword);
			Assert.IsTrue(!string.IsNullOrEmpty(obtainedToken));

		}

		[TestMethod]
		[ExpectedException(typeof(InvalidCredentialsException))]
		public void Login_EmailNotFound_ExceptionThrown()
		{
			string expectedToken = "token1234";
			string queryEmail = "a@a.com";
			string queryPassword = "1234";

			User expectedUser = new User()
			{
				Email = queryEmail,
				Id = 1,
				Password = queryPassword,
				Name = "test"
			};

			Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userRepositoryMock.Setup(m => m.Get(queryEmail)).Throws(new NotFoundException(queryEmail));

			Session expectedSession = new Session()
			{
				Id = 1,
				Token = expectedToken,
				User = expectedUser
			};

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByEmail(queryEmail)).Throws(new NotFoundException(queryEmail));

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, userRepositoryMock.Object);

			string obtainedToken = sessionLogic.Login(queryEmail, queryPassword);
			Assert.IsNull(obtainedToken);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidCredentialsException))]
		public void Login_WrongPassword_ExceptionThrown()
		{
			string expectedToken = "token1234";
			string queryEmail = "a@a.com";
			string queryPassword = "1234";

			User expectedUser = new User()
			{
				Email = queryEmail,
				Id = 1,
				Password = "123",
				Name = "test"
			};

			Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userRepositoryMock.Setup(m => m.Get(queryEmail)).Returns(expectedUser);

			Session expectedSession = new Session()
			{
				Id = 1,
				Token = expectedToken,
				User = expectedUser
			};

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByEmail(queryEmail)).Returns(expectedSession);

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, userRepositoryMock.Object);

			string obtainedToken = sessionLogic.Login(queryEmail, queryPassword);
			Assert.IsNull(obtainedToken);
		}

		[TestMethod]
		public void Logout_ExistingSession_LoggedOut()
        {
			string expectedToken = "token1234";
			string queryEmail = "a@a.com";
			string queryPassword = "1234";

			User expectedUser = new User()
			{
				Email = queryEmail,
				Id = 1,
				Password = queryPassword,
				Name = "test"
			};

			Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);
			userRepositoryMock.Setup(m => m.Get(queryEmail)).Returns(expectedUser);

			Session expectedSession = new Session()
			{
				Id = 1,
				Token = expectedToken,
				User = expectedUser
			};

			Mock<ISessionRepository> sessionRepositoryMockBeforeLogout = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMockBeforeLogout.Setup(m => m.GetByToken(expectedToken)).Returns(expectedSession);
			sessionRepositoryMockBeforeLogout.Setup(m => m.Delete(expectedSession));

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMockBeforeLogout.Object, userRepositoryMock.Object);

			sessionLogic.Logout(expectedToken);

			Session deletedSession = null;

			Mock<ISessionRepository> sessionRepositoryMockAfterLogout = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMockAfterLogout.Setup(m => m.GetByToken(expectedToken)).Returns(deletedSession);
			sessionRepositoryMockAfterLogout.Setup(m => m.GetByEmail(expectedSession.GetSessionEmail())).Returns(deletedSession);
			sessionRepositoryMockAfterLogout.Setup(m => m.Add(It.IsAny<Session>()));

			sessionLogic = new SessionLogic(sessionRepositoryMockAfterLogout.Object, userRepositoryMock.Object);

			string newToken = sessionLogic.Login(queryEmail, queryPassword);

			Assert.AreNotEqual(expectedToken, newToken);
		}

		[TestMethod]
		public void IsTokenValid_ValidExists_True()
		{
			string tokenToSearch = "token1234";
			Session expectedSession = new Session()
			{
				Id = 1,
				Token = tokenToSearch,
				User = new User()
				{
					Email = "a@a.com",
					Id = 1,
					Password = "1234",
					Name = "test"
				}
			};

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByToken(tokenToSearch)).Returns(expectedSession);
			Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, userRepositoryMock.Object);

			bool isValidToken = sessionLogic.IsTokenValid(tokenToSearch);
			sessionRepositoryMock.VerifyAll();

			Assert.IsTrue(isValidToken);
		}

		[TestMethod]
		public void TokenHasRole_HasRole_True()
		{
			string targetRole = "Administrator";
			List<Role> expectedRoles = new List<Role>()
			{
				new Role()
				{
					Id = 1,
					Name = targetRole
				}
			};
			string token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4";

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetRoles(token)).Returns(expectedRoles);
			Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, userRepositoryMock.Object);

			bool hasRole = sessionLogic.TokenHasRole(token, targetRole);
			sessionRepositoryMock.VerifyAll();

			Assert.IsTrue(hasRole);
		}

		[TestMethod]
		public void TokenHasRole_NoHasRole_False()
		{
			string targetRole = "Administrator";
			List<Role> expectedRoles = new List<Role>()
			{
				new Role()
				{
					Id = 1,
					Name = "AnotherRole"
				}
			};
			string token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4";

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetRoles(token)).Returns(expectedRoles);
			Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>(MockBehavior.Strict);

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, userRepositoryMock.Object);

			bool hasRole = sessionLogic.TokenHasRole(token, targetRole);
			sessionRepositoryMock.VerifyAll();

			Assert.IsFalse(hasRole);
		}
	}
}
