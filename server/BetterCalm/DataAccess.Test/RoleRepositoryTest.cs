using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Domain.Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

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
        public void Get_RoleFound_Fetched()
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
        public void Get_RoleNotFound_ExceptionThrown()
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

        [TestMethod]
        public void GetUsersByRole_UsersAndRolesExist_Fetched()
        {
            string roleName = "Administrator";
            Role role = new Role()
            {
                Id = 1,
                Name = roleName
            };
            this.context.Add(role);

            User user1 = new User()
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
            User user2 = new User()
            {
                Email = "b@b.com",
                Id = 2,
                Password = "1234Test",
                Name = "test"
            };

            this.context.Add(user1);
            this.context.Add(user2);
            this.context.SaveChanges();

            List<User> expectedUsers = new List<User>()
            {
                user1
            };

            RoleRepository repository = new RoleRepository(this.context);

            ICollection<User> obtainedUsers = repository.GetUsers(roleName);

            CollectionAssert.AreEqual(expectedUsers, obtainedUsers.ToList());
        }
    }
}
