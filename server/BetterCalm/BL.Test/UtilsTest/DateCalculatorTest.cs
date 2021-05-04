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
    }
}
