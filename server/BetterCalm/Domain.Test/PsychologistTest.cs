using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
	[TestClass]
	public class PsychologistTest
	{
		[TestMethod]
		public void GetLastOk()
		{
            Schedule schedule = new Schedule()
            {
                Date = new DateTime(2021, 04, 28)
            };

            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 01),
                ScheduleDays = new List<Schedule>()
                {
                    schedule
                }
            };

            Schedule obtained = psychologist.GetLast();
            Assert.AreEqual(schedule, obtained);
        }

        [TestMethod]
        public void GetLastFailed()
        {
            Schedule schedule = new Schedule()
            {
                Date = new DateTime(2021, 04, 28)
            };

            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 01)
            };

            Schedule obtained = psychologist.GetLast();
            Assert.AreNotEqual(schedule, obtained);
        }

        [TestMethod]
        public void GetFullNameOk()
        {

            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 01)
            };

            string expectedFullName = "Juan Sartori";
            string fullName = psychologist.GetFullName();

            Assert.AreEqual(expectedFullName, fullName);
        }
    }
}
