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
		public void GetLast_HasSchedule_Fetched()
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
        public void GetLast_DoesNotHaveSchedule_NotEquals()
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
        public void GetFullName_HasFirstAndLastName_Fetched()
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


        [TestMethod]
        public void UpdateData_DataIsCorrectAllProperties_Updated()
        {
            Illness stress = new Illness() { Id = 1, Name = "Stress" };
            Illness depression = new Illness() { Id = 2, Name = "Depression" };
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                Illnesses = new List<Illness>() { stress },
                CreatedDate = new DateTime(2021, 01, 01)
            };

            Psychologist newPsychologist = new Psychologist()
            {
                Id = 2,
                FirstName = "Orestes",
                LastName = "Fiandra",
                Address = "General Paz 1234",
                Format = Format.Remote,
                Illnesses = new List<Illness>() { depression },
                CreatedDate = new DateTime(2011, 01, 01)
            };

            psychologist.UpdateData(newPsychologist);

            Assert.IsTrue(Equals(psychologist.FirstName, newPsychologist.FirstName) 
                && Equals(psychologist.LastName, newPsychologist.LastName)
                && Equals(psychologist.Address, newPsychologist.Address)
                && Equals(psychologist.Format, newPsychologist.Format)
                && Equals(psychologist.Illnesses, newPsychologist.Illnesses));
        }

        [TestMethod]
        public void UpdateData_DataHasNullOrEmpty_EmptyOrNullDataNotUpdated()
        {
            Illness stress = new Illness() { Id = 1, Name = "Stress" };
            Illness depression = new Illness() { Id = 2, Name = "Depression" };
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                Illnesses = new List<Illness>() { stress },
                CreatedDate = new DateTime(2021, 01, 01)
            };

            Psychologist newPsychologist = new Psychologist()
            {
                Id = 2,
                FirstName = "",
                LastName = null,
                Address = "",
                Format = Format.Remote,
                Illnesses = null,
                CreatedDate = new DateTime(2011, 01, 01)
            };

            psychologist.UpdateData(newPsychologist);

            Assert.IsTrue(!Equals(psychologist.FirstName, newPsychologist.FirstName)
                && !Equals(psychologist.LastName, newPsychologist.LastName)
                && !Equals(psychologist.Address, newPsychologist.Address)
                && Equals(psychologist.Format, newPsychologist.Format)
                && !Equals(psychologist.Illnesses, newPsychologist.Illnesses));
        }
    }
}
