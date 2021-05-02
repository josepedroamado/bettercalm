using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Domain.Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace DataAccess.Test
{
	[TestClass]
	public class RoleRepositoryTest
	{
        private DbContext context;
        private DbConnection connection;

        public RoleRepositoryTest()
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
        public void GetRoleOk()
		{
            Role role = new Role()
            {
                Id = 1,
                Name = "Administrator",
                Description = "Administrator role"
            };

            this.context.Add(role);
            this.context.SaveChanges();

            RoleRepository repository = new RoleRepository(this.context);

            Role obtainedRole = repository.Get(role.Name);

            Assert.AreEqual(obtainedRole, role);
		}

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetRoleNotFound()
        {
            Role role = new Role()
            {
                Id = 1,
                Name = "Administrator",
                Description = "Administrator role"
            };

            this.context.Add(role);
            this.context.SaveChanges();

            RoleRepository repository = new RoleRepository(this.context);

            Role obtainedRole = repository.Get("patient");

            Assert.AreEqual(obtainedRole, role);
        }
    }
}
