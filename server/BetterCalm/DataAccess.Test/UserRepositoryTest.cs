using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Domain.Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Common;

namespace DataAccess.Test
{
	[TestClass]
	public class UserRepositoryTest
	{
        private DbContext context;
        private DbConnection connection;

        public UserRepositoryTest()
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
            User expectedUser = new User()
            {
                EMail = "a@a.com",
                Id = 1,
                Password = "1234Test",
                Name = "test"
            };

            this.context.Add(expectedUser);
            this.context.SaveChanges();

            UserRepository repository = new UserRepository(this.context);

            User obtainedUser = repository.Get(expectedUser.EMail);

            Assert.AreEqual(expectedUser, obtainedUser);
		}


        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetNotFound()
        {
            string expectedUserEmail = "a@a.com";

            UserRepository repository = new UserRepository(this.context);

            User obtainedUser = repository.Get(expectedUserEmail);

            Assert.IsNull(obtainedUser);
        }

        [TestMethod]
        public void AddOk()
		{
            User user = new User()
            {
                Id = 1,
                EMail = "test@test.com",
                Password = "test1234",
                Name = "test",
                Roles = new List<Role>()
                {
                    new Role()
                    {
                        Id = 1,
                        Name = "Administrator"
                    }
                }
            };

            UserRepository repository = new UserRepository(this.context);
            repository.Add(user);
            User obtainedUser = repository.Get(user.EMail);

            Assert.AreEqual(user, obtainedUser);
		}

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsException))]
        public void AddAlreadyExists()
        {
            User user = new User()
            {
                Id = 1,
                EMail = "test@test.com",
                Password = "test1234",
                Name = "test",
                Roles = new List<Role>()
                {
                    new Role()
                    {
                        Id = 1,
                        Name = "Administrator"
                    }
                }
            };

            UserRepository repository = new UserRepository(this.context);
            repository.Add(user);
            repository.Add(user);

            User obtainedUser = repository.Get(user.EMail);

            Assert.AreEqual(user, obtainedUser);
        }

        [TestMethod]
        public void UpdateOk()
        {
            User user = new User()
            {
                Id = 1,
                EMail = "test@test.com",
                Password = "test1234",
                Name = "test",
                Roles = new List<Role>()
                {
                    new Role()
                    {
                        Id = 1,
                        Name = "Administrator"
                    }
                }
            };

            UserRepository repository = new UserRepository(this.context);
            repository.Add(user);

            user.Password = "test12345";
            repository.Update(user);
           
            User obtainedUser = repository.Get(user.EMail);

            Assert.AreEqual(user.Password, obtainedUser.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void UpdateNotFound()
        {
            User user = new User()
            {
                Id = 1,
                EMail = "test@test.com",
                Password = "test1234",
                Name = "test",
                Roles = new List<Role>()
                {
                    new Role()
                    {
                        Id = 1,
                        Name = "Administrator"
                    }
                }
            };

            UserRepository repository = new UserRepository(this.context);

            user.Password = "test12345";
            repository.Update(user);

            User obtainedUser = repository.Get(user.EMail);

            Assert.IsNull(obtainedUser);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void DeleteOk()
        {
            User expectedUser = new User()
            {
                EMail = "a@a.com",
                Id = 1,
                Password = "1234Test",
                Name = "test"
            };

            this.context.Add(expectedUser);
            this.context.SaveChanges();

            UserRepository repository = new UserRepository(this.context);

            repository.Delete(expectedUser.Id);
            User obtainedUser = repository.Get(expectedUser.EMail);

            Assert.IsNull(obtainedUser);
        }
    }
}
