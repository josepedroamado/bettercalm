using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccess.Test
{
    [TestClass]
	public class SessionRepositoryTest
	{
        private DbContext context;
        private DbConnection connection;

        public SessionRepositoryTest()
        {
            this.connection = new SqliteConnection("Filename=:memory:");
            this.context = new BetterCalmContext(
                new DbContextOptionsBuilder<BetterCalmContext>()
                .UseSqlite(connection)
                .Options);
        }

        [TestInitialize]
        public void Setup()
        {
            this.connection.Open();
            this.context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetByEmail_SessionFound_Fetched()
		{
            Session expectedSession = new Session()
            {
                Id = 1,
                Token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4",
                User = new User()
				{
                    Id = 1,
                    EMail = "a@a.com",
                    Password = "1234",
                    Name = "test"
                }
            };

            this.context.Add(expectedSession);
            this.context.SaveChanges();

            SessionRepository repository = new SessionRepository(this.context);

            Session obtainedSession = repository.GetByEmail(expectedSession.GetSessionEmail());

            Assert.AreEqual(expectedSession, obtainedSession);
		}

        [TestMethod]
        public void GetByEmail_SessionNotFound_ReturnsNull()
		{
            string expectedSessionEMail = "a@a.com";
            SessionRepository repository = new SessionRepository(this.context);

            Session obtainedSession = repository.GetByEmail(expectedSessionEMail);

            Assert.IsNull(obtainedSession);
        }

        [TestMethod]
        public void GetByToken_SessionFound_Fetched()
        {
            Session expectedSession = new Session()
            {
                Id = 1,
                Token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4",
                User = new User()
                {
                    Id = 1,
                    EMail = "a@a.com",
                    Password = "1234",
                    Name = "test"
                }
            };

            this.context.Add(expectedSession);
            this.context.SaveChanges();

            SessionRepository repository = new SessionRepository(this.context);

            Session obtainedSession = repository.GetByToken(expectedSession.Token);

            Assert.AreEqual(expectedSession, obtainedSession);
        }

        [TestMethod]
        public void GetByToken_SessionNotFound_ReturnsNull()
        {
            string expectedToken = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4";
            SessionRepository repository = new SessionRepository(this.context);

            Session obtainedSession = repository.GetByToken(expectedToken);

            Assert.IsNull(obtainedSession);
        }

        [TestMethod]
        public void Add_DataIsCorrect_Added()
		{
            Session session = new Session()
            {
                Id = 1,
                Token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4",
                User = new User()
                {
                    Id = 1,
                    EMail = "a@a.com",
                    Password = "1234",
                    Name = "test"
                }
            };

            SessionRepository repository = new SessionRepository(this.context);
            repository.Add(session);

            Session obtainedSession = repository.GetByEmail(session.GetSessionEmail());

            Assert.AreEqual(session, obtainedSession);
        }

        [TestMethod]
        public void Delete_SessionFound_Deleted()
        {
            Session session = new Session()
            {
                Id = 1,
                Token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4",
                User = new User()
                {
                    Id = 1,
                    EMail = "a@a.com",
                    Password = "1234",
                    Name = "test"
                }
            };

            SessionRepository repository = new SessionRepository(this.context);
            repository.Add(session);

            repository.Delete(session);
            Session obtainedSession = repository.GetByEmail(session.GetSessionEmail());

            Assert.IsNull(obtainedSession);
        }

        [TestMethod]
        public void GetRoles_HasRole_Fetched()
		{
            string targetRole = "Administrator";
            Session session = new Session()
            {
                Id = 1,
                Token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4",
                User = new User()
                {
                    Id = 1,
                    EMail = "a@a.com",
                    Password = "1234",
                    Name = "test",
                    Roles = new List<Role>()
					{
                        new Role()
						{
                            Id = 1,
                            Name = targetRole
                        }
					}
                }
            };

            SessionRepository repository = new SessionRepository(this.context);
            repository.Add(session);

            Assert.IsTrue(repository.GetRoles(session.Token).Any(role => role.Name.Equals(targetRole)));
        }

        [TestMethod]
        public void GetRoles_NoHasRole_Fetched()
        {
            string targetRole = "Administrator";
            Session session = new Session()
            {
                Id = 1,
                Token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4",
                User = new User()
                {
                    Id = 1,
                    EMail = "a@a.com",
                    Password = "1234",
                    Name = "test",
                    Roles = new List<Role>()
                    {
                        new Role()
                        {
                            Id = 1,
                            Name = "AnotherRole"
                        }
                    }
                }
            };

            SessionRepository repository = new SessionRepository(this.context);
            repository.Add(session);

            Assert.IsFalse(repository.GetRoles(session.Token).Any(role => role.Name.Equals(targetRole)));
        }
    }
}
