using DataAccess.Context;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using System;
using DataAccess.Repositories;
using Domain.Exceptions;
using System.Collections.Generic;

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

        [TestMethod]
        public void GetPsychologistByIllnessDateOk()
		{
            Illness illness = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(illness);

            Appointment appointment = new Appointment()
            {
                Id = 1,
                Date = new DateTime(2021, 04, 28)
            };
            this.context.Add(appointment);

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
                    new Schedule()
                    {
                        Appointments = new List<Appointment>() {
                           appointment
                        }
                    }
                },
                Illnesses = new List<Illness>()
                {
                    illness
                }
            };
            this.context.Add(psychologist);

            Psychologist psychologist2 = new Psychologist()
            {
                Id = 2,
                FirstName = "Juan2",
                LastName = "Sartori2",
                Address = "Calle 12342",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 02),
                ScheduleDays = new List<Schedule>()
                {
                    new Schedule()
                    {
                        Appointments = new List<Appointment>() {
                           appointment
                        }
                    }
                },
                Illnesses = new List<Illness>()
                {
                    illness
                }
            };
            this.context.Add(psychologist2);
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(illness, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologist.Id);
        }

        [TestMethod]
        public void GetPsychologistWithoutIllnessDateOk()
        {
            Illness depresion = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(depresion);

            Illness estres = new Illness()
            {
                Id = 2,
                Name = "Estres"
            };
            this.context.Add(estres);

            Appointment appointment = new Appointment()
            {
                Id = 1,
                Date = new DateTime(2021, 04, 28)
            };
            this.context.Add(appointment);

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
                    new Schedule()
                    {
                        Appointments = new List<Appointment>() {
                           appointment
                        }
                    }
                },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologist);

            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(estres, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologist.Id);
        }

        [TestMethod]
        public void GetPsychologistFullSchedule()
        {
            Illness estres = new Illness()
            {
                Id = 1,
                Name = "Estres"
            };
            this.context.Add(estres);

            List<Appointment> appointments = new List<Appointment>()
            {
                new Appointment()
                {
                    Id = 1
                },
                new Appointment()
                {
                    Id = 2
                },
                new Appointment()
                {
                    Id = 3
                },
                new Appointment(){
                    Id = 4
                },
                new Appointment(){
                    Id = 5
                }
            };
            appointments.ForEach(appointment => this.context.Add(appointment));

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
                    new Schedule()
                    {
                        Date = new DateTime(2021, 04, 28),
                        Appointments = appointments
                    } 
                },
                Illnesses = new List<Illness>()
                {
                    estres
                }
            };
            this.context.Add(psychologist);
            Psychologist psychologist2 = new Psychologist()
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 02),
                ScheduleDays = new List<Schedule>(),
                Illnesses = new List<Illness>()
                {
                    estres
                }
            };
            this.context.Add(psychologist2);

            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(estres, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologist2.Id);
        }
    }
}
