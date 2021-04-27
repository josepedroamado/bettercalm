using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BL.Test
{
	[TestClass]
	public class UserManagertest
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

			UserManager userManager = new UserManager(sessionRepositoryMock.Object, administratorRepositoryMock.Object);

			string obtainedToken = userManager.Login(queryEmail, queryPassword);
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

			UserManager userManager = new UserManager(sessionRepositoryMock.Object, administratorRepositoryMock.Object);

			string obtainedToken = userManager.Login(queryEmail, queryPassword);
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

			UserManager userManager = new UserManager(sessionRepositoryMock.Object, administratorRepositoryMock.Object);

			string obtainedToken = userManager.Login(queryEmail, queryPassword);
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

			UserManager userManager = new UserManager(sessionRepositoryMock.Object, administratorRepositoryMock.Object);

			string obtainedToken = userManager.Login(queryEmail, queryPassword);
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

			UserManager userManager = new UserManager(sessionRepositoryMockBeforeLogout.Object, administratorRepositoryMock.Object);

			userManager.Logout(expectedToken);

			Session deletedSession = null;

			Mock<ISessionRepository> sessionRepositoryMockAfterLogout = new Mock<ISessionRepository>(MockBehavior.Strict);
			sessionRepositoryMockAfterLogout.Setup(m => m.GetByToken(expectedToken)).Returns(deletedSession);
			sessionRepositoryMockAfterLogout.Setup(m => m.GetByEmail(expectedSession.GetSessionEmail())).Returns(deletedSession);
			sessionRepositoryMockAfterLogout.Setup(m => m.Add(It.IsAny<Session>()));

			userManager = new UserManager(sessionRepositoryMockAfterLogout.Object, administratorRepositoryMock.Object);

			string newToken = userManager.Login(queryEmail, queryPassword);

			Assert.AreNotEqual(expectedToken, newToken);
		}
	}
}
