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
using System.Linq;

namespace DataAccess.Test
{
	[TestClass]
    public class AppointmentDurationRepositoryTest
    {
        private DbContext context;
        private DbConnection connection;

		public AppointmentDurationRepositoryTest()
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
        public void GetAll_DurationsExist_Fetched()
        {
            List<AppointmentDuration> expectedDurations = GetAllExpectedDurations();
            
            foreach(AppointmentDuration duration in expectedDurations)
            {
                this.context.Add(duration);
            }
            this.context.SaveChanges();
            AppointmentDurationRepository appointmentDurationRepository = new AppointmentDurationRepository(this.context);

            IEnumerable<AppointmentDuration> obtainedDurations = appointmentDurationRepository.GetAll();
            Assert.AreEqual(expectedDurations.Count, obtainedDurations.Count());
        }

        private List<AppointmentDuration> GetAllExpectedDurations()
        {
            AppointmentDuration hour = new AppointmentDuration()
            {
                Id = 1,
                Duration = new TimeSpan(1, 0, 0)
            };
            AppointmentDuration hourAndAHalf =new AppointmentDuration()
            {
                Id = 2,
                Duration = new TimeSpan(1, 30, 0)
            };               
            AppointmentDuration twoHours =new AppointmentDuration()
            {
                Id = 3,
                Duration = new TimeSpan(2, 0, 0)
            };
            List<AppointmentDuration> durations = new List<AppointmentDuration>()
            {
                hour, hourAndAHalf, twoHours
            };
            return durations;
        }
    }
}
