using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BL.Test
{
	[TestClass]
	public class SessionLogicTest
	{

		[TestMethod]
		public void LoginOk()
		{
			string expectedToken = "token1234";
			string queryEmail = "a@a.com";
			string queryPassword = "1234";

			Administrator expectedAdministrator = new Administrator()
			{
				EMail = queryEmail,
				Id = 1,
				Password = queryPassword
			};

			Mock<IAdministratorRepository> administratorRepositoryMock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
			administratorRepositoryMock.Setup(m => m.Get(queryEmail)).Returns(expectedAdministrator);

			Session expectedSession = new Session()
			{
				Id = 1,
				Token = expectedToken,
				User = expectedAdministrator
			};

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByEmail(queryEmail)).Returns(expectedSession);

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, administratorRepositoryMock.Object);

			string obtainedToken = sessionLogic.Login(queryEmail, queryPassword);
			Assert.AreEqual(expectedToken, obtainedToken);

		}

		[TestMethod]
		public void LoginOkCreatingSession()
		{
			string queryEmail = "a@a.com";
			string queryPassword = "1234";

			Administrator expectedAdministrator = new Administrator()
			{
				EMail = queryEmail,
				Id = 1,
				Password = queryPassword
			};

			Mock<IAdministratorRepository> administratorRepositoryMock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
			administratorRepositoryMock.Setup(m => m.Get(queryEmail)).Returns(expectedAdministrator);

			Session expectedSession = null;

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByEmail(queryEmail)).Returns(expectedSession);
			sessionRepositoryMock.Setup(m => m.Add(It.IsAny<Session>()));

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, administratorRepositoryMock.Object);

			string obtainedToken = sessionLogic.Login(queryEmail, queryPassword);
			Assert.IsTrue(!string.IsNullOrEmpty(obtainedToken));

		}

		[TestMethod]
		[ExpectedException(typeof(InvalidCredentialsException))]
		public void LoginFailedEmailNotFound()
		{
			string expectedToken = "token1234";
			string queryEmail = "a@a.com";
			string queryPassword = "1234";

			Administrator expectedAdministrator = new Administrator()
			{
				EMail = queryEmail,
				Id = 1,
				Password = queryPassword
			};

			Mock<IAdministratorRepository> administratorRepositoryMock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
			administratorRepositoryMock.Setup(m => m.Get(queryEmail)).Throws(new NotFoundException(queryEmail));

			Session expectedSession = new Session()
			{
				Id = 1,
				Token = expectedToken,
				User = expectedAdministrator
			};

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByEmail(queryEmail)).Throws(new NotFoundException(queryEmail));

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, administratorRepositoryMock.Object);

			string obtainedToken = sessionLogic.Login(queryEmail, queryPassword);
			Assert.IsNull(obtainedToken);
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidCredentialsException))]
		public void LoginFailedWrongPassword()
		{
			string expectedToken = "token1234";
			string queryEmail = "a@a.com";
			string queryPassword = "1234";

			Administrator expectedAdministrator = new Administrator()
			{
				EMail = queryEmail,
				Id = 1,
				Password = "123"
			};

			Mock<IAdministratorRepository> administratorRepositoryMock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
			administratorRepositoryMock.Setup(m => m.Get(queryEmail)).Returns(expectedAdministrator);

			Session expectedSession = new Session()
			{
				Id = 1,
				Token = expectedToken,
				User = expectedAdministrator
			};

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByEmail(queryEmail)).Returns(expectedSession);

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, administratorRepositoryMock.Object);

			string obtainedToken = sessionLogic.Login(queryEmail, queryPassword);
			Assert.IsNull(obtainedToken);
		}

		[TestMethod]
		public void LogoutOk()
        {
			string expectedToken = "token1234";
			string queryEmail = "a@a.com";
			string queryPassword = "1234";

			Administrator expectedAdministrator = new Administrator()
			{
				EMail = queryEmail,
				Id = 1,
				Password = queryPassword
			};

			Mock<IAdministratorRepository> administratorRepositoryMock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
			administratorRepositoryMock.Setup(m => m.Get(queryEmail)).Returns(expectedAdministrator);

			Session expectedSession = new Session()
			{
				Id = 1,
				Token = expectedToken,
				User = expectedAdministrator
			};

			Mock<ISessionRepository> sessionRepositoryMockBeforeLogout = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMockBeforeLogout.Setup(m => m.GetByToken(expectedToken)).Returns(expectedSession);
			sessionRepositoryMockBeforeLogout.Setup(m => m.Delete(expectedSession));

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMockBeforeLogout.Object, administratorRepositoryMock.Object);

			sessionLogic.Logout(expectedToken);

			Session deletedSession = null;

			Mock<ISessionRepository> sessionRepositoryMockAfterLogout = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMockAfterLogout.Setup(m => m.GetByToken(expectedToken)).Returns(deletedSession);
			sessionRepositoryMockAfterLogout.Setup(m => m.GetByEmail(expectedSession.GetSessionEmail())).Returns(deletedSession);
			sessionRepositoryMockAfterLogout.Setup(m => m.Add(It.IsAny<Session>()));

			sessionLogic = new SessionLogic(sessionRepositoryMockAfterLogout.Object, administratorRepositoryMock.Object);

			string newToken = sessionLogic.Login(queryEmail, queryPassword);

			Assert.AreNotEqual(expectedToken, newToken);
		}

		[TestMethod]
		public void ValidateTokenOk()
		{
			;

			string tokenToSearch = "token1234";
			Session expectedSession = new Session()
			{
				Id = 1,
				Token = tokenToSearch,
				User = new Administrator()
				{
					EMail = "a@a.com",
					Id = 1,
					Password = "1234"
				}
			};

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByToken(tokenToSearch)).Returns(expectedSession);
			Mock<IAdministratorRepository> administratorRepositoryMock = new Mock<IAdministratorRepository>(MockBehavior.Strict);

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, administratorRepositoryMock.Object);

			bool isValidToken = sessionLogic.IsValidToken(tokenToSearch);
			sessionRepositoryMock.VerifyAll();

			Assert.IsTrue(isValidToken);
		}

		[TestMethod]
		public void ValidateTokenNotFoundOk()
		{
			;

			string tokenToSearch = "token1234";
			Session expectedSession = null;

			Mock<ISessionRepository> sessionRepositoryMock = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMock.Setup(m => m.GetByToken(tokenToSearch)).Returns(expectedSession);
			Mock<IAdministratorRepository> administratorRepositoryMock = new Mock<IAdministratorRepository>(MockBehavior.Strict);

			SessionLogic sessionLogic = new SessionLogic(sessionRepositoryMock.Object, administratorRepositoryMock.Object);

			bool isValidToken = sessionLogic.IsValidToken(tokenToSearch);
			sessionRepositoryMock.VerifyAll();

			Assert.IsFalse(isValidToken);
		}
	}
}
