using DataAccess.Context;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using System;
using DataAccess.Repositories;
using Domain.Exceptions;

namespace DataAccess.Test
{
    [TestClass]
    public class PsychologistRepositoryTest
    {
        private DbContext context;
        private DbConnection connection;

        public PsychologistRepositoryTest()
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
            Psychologist expectedPsychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            this.context.Add(expectedPsychologist);
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtainedPsychologist = repository.Get(expectedPsychologist.Id);

            Assert.AreEqual(expectedPsychologist, obtainedPsychologist);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetNotFound()
        {
            int expectedPsychologistId = 1;

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtainedPsychologist = repository.Get(expectedPsychologistId);

            Assert.IsNull(obtainedPsychologist);
        }

        [TestMethod]
        public void AddOk()
        {
            Psychologist expectedPsychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(expectedPsychologist);

            Psychologist obtainedPsychologist = repository.Get(expectedPsychologist.Id);

            Assert.AreEqual(expectedPsychologist, obtainedPsychologist);
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsException))]
        public void AddAlreadyExists()
        {
            Psychologist expectedPsychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(expectedPsychologist);
            repository.Add(expectedPsychologist);

            Psychologist obtainedPsychologist = repository.Get(expectedPsychologist.Id);

            Assert.AreEqual(expectedPsychologist, obtainedPsychologist);
        }
    }
}
