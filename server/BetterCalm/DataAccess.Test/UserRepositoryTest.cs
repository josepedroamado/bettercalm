﻿using DataAccess.Context;
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
                Password = "1234Test"
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
    }
}