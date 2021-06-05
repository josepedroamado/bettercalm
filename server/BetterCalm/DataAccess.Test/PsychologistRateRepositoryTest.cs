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
    public class PsychologistRateRepositoryTest
    {
        private DbContext context;
        private DbConnection connection;

        public PsychologistRateRepositoryTest()
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
        public void GetAll_RatesExist_Fetched()
        {
            List<PsychologistRate> expectedRates = GetAllExpectedRates();

            foreach (PsychologistRate rate in expectedRates)
            {
                this.context.Add(rate);
            }
            this.context.SaveChanges();
            PsychologistRateRepository ratesRepository = new PsychologistRateRepository(this.context);

            IEnumerable<PsychologistRate> obtainedRates = ratesRepository.GetAll();
            Assert.IsTrue(expectedRates.SequenceEqual(obtainedRates));
        }

        private List<PsychologistRate> GetAllExpectedRates()
        {
            PsychologistRate fiveHundred = new PsychologistRate()
            {
                Id = 1,
                HourlyRate = 500
            };
            PsychologistRate seventyfiveHundred = new PsychologistRate()
            {
                Id = 2,
                HourlyRate = 750
            };
            PsychologistRate thousand = new PsychologistRate()
            {
                Id = 3,
                HourlyRate = 1000
            };
            PsychologistRate twoThousand = new PsychologistRate()
            {
                Id = 4,
                HourlyRate = 2000
            };
            List<PsychologistRate> rates = new List<PsychologistRate>()
            {
                fiveHundred, seventyfiveHundred, thousand, twoThousand 
            };
            return rates;
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void GetAll_NoRates_ExceptionThrown()
        {
            PsychologistRateRepository ratesRepository = new PsychologistRateRepository(this.context);
            IEnumerable<PsychologistRate> obtainedRates = ratesRepository.GetAll();
            Assert.IsNull(obtainedRates);
        }

        [TestMethod]
        public void Get_RateExists_Fetched()
        {
            PsychologistRate expectedRate = GetExpectedRate();
            this.context.Add(expectedRate);
            this.context.SaveChanges();
            PsychologistRateRepository ratesRepository = new PsychologistRateRepository(this.context);

            PsychologistRate obtainedRate = ratesRepository.Get(expectedRate.HourlyRate);

            Assert.AreEqual(expectedRate, obtainedRate);
        }

        private PsychologistRate GetExpectedRate()
        {
            return new PsychologistRate()
            {
                Id = 1,
                HourlyRate = 1000
            };
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Get_RateNotFound_ExceptionThrown()
        {
            PsychologistRate expectedRate = GetExpectedRate();
            PsychologistRateRepository ratesRepository = new PsychologistRateRepository(this.context);

            PsychologistRate obtainedRate = ratesRepository.Get(expectedRate.HourlyRate);

            Assert.IsNull(obtainedRate);
        }
    }
}
