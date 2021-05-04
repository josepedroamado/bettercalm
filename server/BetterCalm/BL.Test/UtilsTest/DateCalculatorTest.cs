using BL.Utils;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

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

        [TestMethod]
        public void CalculateAppointmentDate_PsychologistEmptySchedule_Ok()
        {
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(1990, 5, 10),
                ScheduleDays = new List<Schedule>()
            };
            DateTime expectedDate = DateCalculator.CalculateNextWorkDay(DateTime.Now.Date.AddDays(1));
            DateTime calculatedDate = DateCalculator.CalculateAppointmentDate(psychologist, 5);
            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void CalculateAppointmentDate_PsychologistLastAppointmentDateIsOlder_Ok()
        {
            DateTime date = DateCalculator.CalculateNextWorkDay(DateTime.Now.Date.AddDays(-1));
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(1990, 5, 10),
                ScheduleDays = new List<Schedule>()
                {
                    new Schedule()
                    {
                        Id = 1,
                        Date = date
                    }
                }
            };
            DateTime expectedDate = DateCalculator.CalculateNextWorkDay(DateTime.Now.Date.AddDays(1));
            DateTime calculatedDate = DateCalculator.CalculateAppointmentDate(psychologist, 5);
            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void CalculateAppointmentDate_PsychologistLastAppointmentDateIsGreaterAndCountIsBelowLimit_Ok()
        {
            DateTime date = DateCalculator.CalculateNextWorkDay(DateTime.Now.Date.AddDays(2));
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(1990, 5, 10),
                ScheduleDays = new List<Schedule>()
                {
                    new Schedule()
                    {
                        Id = 1,
                        Date = date
                    },
                }
            };
            DateTime expectedDate = date;
            DateTime calculatedDate = DateCalculator.CalculateAppointmentDate(psychologist, 5);
            Assert.AreEqual(expectedDate, calculatedDate);
        }

        [TestMethod]
        public void CalculateAppointmentDate_PsychologistLastAppointmentDateIsGreaterAndCountIsAboveLimit_Ok()
        {
            DateTime date = DateCalculator.CalculateNextWorkDay(DateTime.Now.Date.AddDays(2));
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(1990, 5, 10),
                ScheduleDays = new List<Schedule>()
                {
                    new Schedule()
                    {
                        Id = 1,
                        Date = date,
                        Appointments = new List<Appointment>()
                        {
                            new Appointment()
                        }
                    },
                }
            };
            DateTime expectedDate = DateCalculator.CalculateNextWorkDay(date.AddDays(1));
            DateTime calculatedDate = DateCalculator.CalculateAppointmentDate(psychologist, 1);
            Assert.AreEqual(expectedDate, calculatedDate);
        }
    }
}
