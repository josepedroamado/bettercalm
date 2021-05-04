using BL.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BL.Test.UtilsTest
{
    [TestClass]
    public class DateCalculatorTest
    {
        [TestMethod]
        public void CalculateUntilDate_SinceIsTuesday_Ok()
        {
            DateTime tuesday = new DateTime(2021, 5, 4);
            DateTime expectedDate = new DateTime(2021, 5, 7);
            DateTime calculatedDate = DateCalculator.CalculateUntilDate(tuesday);
            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void CalculateUntilDate_SinceIsFriday_Ok()
        {
            DateTime friday = new DateTime(2021, 5, 7);
            DateTime expectedDate = new DateTime(2021, 5, 14);
            DateTime calculatedDate = DateCalculator.CalculateUntilDate(friday);
            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void CalculateUntilDate_SinceIsSaturday_Ok()
        {
            DateTime saturday = new DateTime(2021, 5, 8);
            DateTime expectedDate = new DateTime(2021, 5, 14);
            DateTime calculatedDate = DateCalculator.CalculateUntilDate(saturday);
            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void CalculateUntilDate_SinceIsSunday_Ok()
        {
            DateTime sunday = new DateTime(2021, 5, 9);
            DateTime expectedDate = new DateTime(2021, 5, 14);
            DateTime calculatedDate = DateCalculator.CalculateUntilDate(sunday);
            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void CalculateNextWorkDay_DateIsMonday_Ok()
        {
            DateTime monday = new DateTime(2021, 5, 3);
            DateTime expectedDate = new DateTime(2021, 5, 3);
            DateTime calculatedDate = DateCalculator.CalculateNextWorkDay(monday);
            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void CalculateNextWorkDay_DateIsFriday_Ok()
        {
            DateTime friday = new DateTime(2021, 5, 7);
            DateTime expectedDate = new DateTime(2021, 5, 7);
            DateTime calculatedDate = DateCalculator.CalculateNextWorkDay(friday);
            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void CalculateNextWorkDay_DateIsSaturday_Ok()
        {
            DateTime saturday = new DateTime(2021, 5, 8);
            DateTime expectedDate = new DateTime(2021, 5, 10);
            DateTime calculatedDate = DateCalculator.CalculateNextWorkDay(saturday);
            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void CalculateNextWorkDay_DateIsSunday_Ok()
        {
            DateTime sunday = new DateTime(2021, 5, 9);
            DateTime expectedDate = new DateTime(2021, 5, 10);
            DateTime calculatedDate = DateCalculator.CalculateNextWorkDay(sunday);
            Assert.AreEqual(expectedDate, calculatedDate);
        }
    }
}
