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
    public class AppointmentDiscountRepositoryTest
    {
        private DbContext context;
        private DbConnection connection;

        public AppointmentDiscountRepositoryTest()
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
        public void GetAll_DiscountsExist_Fetched()
        {
            List<AppointmentDiscount> expectedDiscounts = GetAllExpectedDiscounts();

            foreach (AppointmentDiscount discount in expectedDiscounts)
            {
                this.context.Add(discount);
            }
            this.context.SaveChanges();
            AppointmentDiscountRepository appointmentDiscountRepository = new AppointmentDiscountRepository(this.context);

            IEnumerable<AppointmentDiscount> obtainedDiscounts = appointmentDiscountRepository.GetAll();
            Assert.IsTrue(expectedDiscounts.SequenceEqual(obtainedDiscounts));
        }

        private List<AppointmentDiscount> GetAllExpectedDiscounts()
        {
            AppointmentDiscount fifteen = new AppointmentDiscount()
            {
                Id = 1,
                Discount = 15
            };
            AppointmentDiscount twentyFive = new AppointmentDiscount()
            {
                Id = 2,
                Discount = 25
            };
            AppointmentDiscount fifty = new AppointmentDiscount()
            {
                Id = 3,
                Discount = 50
            };
            List<AppointmentDiscount> discounts = new List<AppointmentDiscount>()
            {
                fifteen, twentyFive, fifty
            };
            return discounts;
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void GetAll_NoDiscounts_ExceptionThrown()
        {
            AppointmentDiscountRepository appointmentDiscountRepository = new AppointmentDiscountRepository(this.context);
            IEnumerable<AppointmentDiscount> obtainedDiscounts = appointmentDiscountRepository.GetAll();
            Assert.IsNull(obtainedDiscounts);
        }

        [TestMethod]
        public void Get_DiscountExists_Fetched()
        {
            AppointmentDiscount expectedDiscount = GetExpectedDiscount();
            this.context.Add(expectedDiscount);
            this.context.SaveChanges();
            AppointmentDiscountRepository appointmentDiscountRepository = new AppointmentDiscountRepository(this.context);

            AppointmentDiscount obtainedDiscount = appointmentDiscountRepository.Get(expectedDiscount.Discount);

            Assert.AreEqual(expectedDiscount, obtainedDiscount);
        }

        private AppointmentDiscount GetExpectedDiscount()
        {
            AppointmentDiscount discount = new AppointmentDiscount()
            {
                Id = 1,
                Discount = 15
            };
            return discount;
        }
    }
}
