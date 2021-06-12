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
        public void Get_UserFound_Fetched()
		{
            User expectedUser = new User()
            {
                Email = "a@a.com",
                Id = 1,
                Password = "1234Test",
                Name = "test"
            };

            this.context.Add(expectedUser);
            this.context.SaveChanges();

            UserRepository repository = new UserRepository(this.context);

            User obtainedUser = repository.Get(expectedUser.Email);

            Assert.AreEqual(expectedUser, obtainedUser);
		}


        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Get_UserNotFound_ExceptionThrown()
        {
            string expectedUserEmail = "a@a.com";

            UserRepository repository = new UserRepository(this.context);

            User obtainedUser = repository.Get(expectedUserEmail);

            Assert.IsNull(obtainedUser);
        }

        [TestMethod]
        public void GetById_UserFound_Fetched()
		{
            User expectedUser = new User()
            {
                Email = "a@a.com",
                Id = 1,
                Password = "1234Test",
                Name = "test"
            };

            this.context.Add(expectedUser);
            this.context.SaveChanges();

            UserRepository repository = new UserRepository(this.context);

            User obtainedUser = repository.Get(expectedUser.Id);

            Assert.AreEqual(expectedUser, obtainedUser);
		}


        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetById_UserNotFound_ExceptionThrown()
        {
            int expectedUserId = 1;

            UserRepository repository = new UserRepository(this.context);

            User obtainedUser = repository.Get(expectedUserId);

            Assert.IsNull(obtainedUser);
        }

        [TestMethod]
        public void Add_DataIsCorrect_Added()
		{
            User user = new User()
            {
                Id = 1,
                Email = "test@test.com",
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
            User obtainedUser = repository.Get(user.Email);

            Assert.AreEqual(user, obtainedUser);
		}

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsException))]
        public void Add_AlreadyExists_ExceptionThrown()
        {
            User user = new User()
            {
                Id = 1,
                Email = "test@test.com",
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

            User obtainedUser = repository.Get(user.Email);

            Assert.AreEqual(user, obtainedUser);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Add_NoNameEntered_ExceptionThrown()
        {
            User user = new User()
            {
                Id = 1,
                Email = "test@test.com",
                Password = "test1234",
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

            User obtainedUser = repository.Get(user.Email);

            Assert.IsNull(obtainedUser);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Add_NoEmailEntered_ExceptionThrown()
        {
            User user = new User()
            {
                Id = 1,
                Name = "name",
                Password = "test1234",
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

            User obtainedUser = repository.Get(user.Email);

            Assert.IsNull(obtainedUser);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Add_NoPasswordEntered_ExceptionThrown()
        {
            User user = new User()
            {
                Id = 1,
                Email = "test@test.com",
                Name = "name",
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

            User obtainedUser = repository.Get(user.Email);

            Assert.IsNull(obtainedUser);
        }


        [TestMethod]
        public void Update_DataIsCorrect_Updated()
        {
            User user = new User()
            {
                Id = 1,
                Email = "test@test.com",
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
           
            User obtainedUser = repository.Get(user.Email);

            Assert.AreEqual(user.Password, obtainedUser.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Update_NoEmailEntered_ExceptionThrown()
        {
            User user = new User()
            {
                Id = 1,
                Email = "test@test.com",
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

            user.Email = string.Empty;
            repository.Update(user);

            User obtainedUser = repository.Get(user.Email);

            Assert.AreNotEqual(user.Email, obtainedUser.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Update_NoPasswordEntered_ExceptionThrown()
        {
            User user = new User()
            {
                Id = 1,
                Email = "test@test.com",
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

            user.Password = string.Empty;
            repository.Update(user);

            User obtainedUser = repository.Get(user.Email);

            Assert.AreNotEqual(user.Password, obtainedUser.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Update_NoNameEntered_ExceptionThrown()
        {
            User user = new User()
            {
                Id = 1,
                Email = "test@test.com",
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

            user.Name = string.Empty;
            repository.Update(user);

            User obtainedUser = repository.Get(user.Email);

            Assert.AreNotEqual(user.Name, obtainedUser.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Update_UserNotFound_ExceptionThrown()
        {
            User user = new User()
            {
                Id = 1,
                Email = "test@test.com",
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

            User obtainedUser = repository.Get(user.Email);

            Assert.IsNull(obtainedUser);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Delete_UserFound_Deleted()
        {
            User expectedUser = new User()
            {
                Email = "a@a.com",
                Id = 1,
                Password = "1234Test",
                Name = "test"
            };

            this.context.Add(expectedUser);
            this.context.SaveChanges();

            UserRepository repository = new UserRepository(this.context);

            repository.Delete(expectedUser.Id);
            User obtainedUser = repository.Get(expectedUser.Email);

            Assert.IsNull(obtainedUser);
        }
    }
}
