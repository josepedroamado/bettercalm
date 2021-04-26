using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Domain.Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;

namespace DataAccess.Test
{
	[TestClass]
	public class AdministratorRepositoryTest
	{
        private DbContext context;
        private DbConnection connection;

        public AdministratorRepositoryTest()
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
        public void GetOk()
		{
            Administrator expectedAdministrator = new Administrator()
            {
                EMail = "a@a.com",
                Id = 1,
                Password = "1234Test"
            };

            this.context.Add(expectedAdministrator);
            this.context.SaveChanges();

            AdministratorRepository repository = new AdministratorRepository(this.context);

            Administrator obtainedAdministrator = repository.Get(expectedAdministrator.EMail);

            Assert.AreEqual(expectedAdministrator, obtainedAdministrator);
		}


        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetNotFound()
        {
            string expectedAdministratorEmail = "a@a.com";

            AdministratorRepository repository = new AdministratorRepository(this.context);

            Administrator obtainedAdministrator = repository.Get(expectedAdministratorEmail);

            Assert.IsNull(obtainedAdministrator);
        }
    }
}
