using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Common;

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
        public void GetByEmailOk()
		{
            Session expectedSession = new Session()
            {
                Id = 1,
                Token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4",
                User = new User()
				{
                    Id = 1,
                    EMail = "a@a.com",
                    Password = "1234"
				}
            };

            this.context.Add(expectedSession);
            this.context.SaveChanges();

            SessionRepository repository = new SessionRepository(this.context);

            Session obtainedSession = repository.GetByEmail(expectedSession.GetSessionEmail());

            Assert.AreEqual(expectedSession, obtainedSession);
		}

        [TestMethod]
        public void GetByEmailNotFound()
		{
            string expectedSessionEMail = "a@a.com";
            SessionRepository repository = new SessionRepository(this.context);

            Session obtainedSession = repository.GetByEmail(expectedSessionEMail);

            Assert.IsNull(obtainedSession);
        }

        [TestMethod]
        public void GetByTokenOk()
        {
            Session expectedSession = new Session()
            {
                Id = 1,
                Token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4",
                User = new User()
                {
                    Id = 1,
                    EMail = "a@a.com",
                    Password = "1234"
                }
            };

            this.context.Add(expectedSession);
            this.context.SaveChanges();

            SessionRepository repository = new SessionRepository(this.context);

            Session obtainedSession = repository.GetByToken(expectedSession.Token);

            Assert.AreEqual(expectedSession, obtainedSession);
        }

        [TestMethod]
        public void GetByTokenNotFound()
        {
            string expectedToken = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4";
            SessionRepository repository = new SessionRepository(this.context);

            Session obtainedSession = repository.GetByToken(expectedToken);

            Assert.IsNull(obtainedSession);
        }

        [TestMethod]
        public void AddOk()
		{
            Session session = new Session()
            {
                Id = 1,
                Token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4",
                User = new User()
                {
                    Id = 1,
                    EMail = "a@a.com",
                    Password = "1234"
                }
            };

            SessionRepository repository = new SessionRepository(this.context);
            repository.Add(session);

            Session obtainedSession = repository.GetByEmail(session.GetSessionEmail());

            Assert.AreEqual(session, obtainedSession);
        }

        [TestMethod]
        public void DeleteOk()
        {
            Session session = new Session()
            {
                Id = 1,
                Token = "B75928B9 - 601A - 438C - 9B0F - C14E56A7BBD4",
                User = new User()
                {
                    Id = 1,
                    EMail = "a@a.com",
                    Password = "1234"
                }
            };

            SessionRepository repository = new SessionRepository(this.context);
            repository.Add(session);

            repository.Delete(session);
            Session obtainedSession = repository.GetByEmail(session.GetSessionEmail());

            Assert.IsNull(obtainedSession);
        }
    }
}
