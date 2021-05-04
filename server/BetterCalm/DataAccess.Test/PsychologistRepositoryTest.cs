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
using System.Linq;

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
        public void GetAll_PsychologistsExist_Fetched()
        {
            List<Psychologist> expectedPsychologists = GetAllExpectedPsychologists();
            expectedPsychologists.ForEach(psychologist => this.context.Add(psychologist));
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            IEnumerable<Psychologist> obtainedPsychologists = repository.GetAll();

            Assert.IsTrue(expectedPsychologists.SequenceEqual(obtainedPsychologists));
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void GetAll_NoPsychologists_ExceptionThrown()
        {
            PsychologistRepository repository = new PsychologistRepository(this.context);
            IEnumerable<Psychologist> obtainedPsychologists = repository.GetAll();
            Assert.IsNull(obtainedPsychologists);
        }
        private List<Psychologist> GetAllExpectedPsychologists()
        {
            Psychologist firstPsychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            Psychologist secondPsychologist = new Psychologist()
            {
                Id = 2,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "14th Street",
                Format = Format.Remote,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };
            List<Psychologist> expectedPsychologists = new List<Psychologist>() { firstPsychologist, secondPsychologist };
            return expectedPsychologists;
        }

        [TestMethod]
        public void Get_PsychologistFound_Fetched()
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
        public void Get_PsychologistNotFound_ExceptionThrown()
        {
            int expectedPsychologistId = 1;

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtainedPsychologist = repository.Get(expectedPsychologistId);

            Assert.IsNull(obtainedPsychologist);
        }

        [TestMethod]
        public void Add_DataIsCorrect_Added()
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
        public void Add_AlreadyExists_ExceptionThrown()
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
        [ExpectedException(typeof(InvalidInputException))]
        public void Add_NoAddress_ExceptionThrown()
        {
            Psychologist expectedPsychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(expectedPsychologist);

            Psychologist obtainedPsychologist = repository.Get(expectedPsychologist.Id);

            Assert.IsNull(obtainedPsychologist);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Add_NoFirstName_ExceptionThrown()
        {
            Psychologist expectedPsychologist = new Psychologist()
            {
                Id = 1,
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(expectedPsychologist);

            Psychologist obtainedPsychologist = repository.Get(expectedPsychologist.Id);

            Assert.IsNull(obtainedPsychologist);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Add_NoLastNAme_ExceptionThrown()
        {
            Psychologist expectedPsychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(expectedPsychologist);

            Psychologist obtainedPsychologist = repository.Get(expectedPsychologist.Id);

            Assert.IsNull(obtainedPsychologist);
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void GetByIllnessAndDate_NoPsychologistsExists_ExceptionThrown()
        {
            Illness depresion = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(depresion);
            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(depresion, new DateTime(2021, 04, 28), 5);

            Assert.IsNull(obtained);
        }

        [TestMethod]
        public void GetByIllnessAndDate_AvailableExpertWithoutSchedule_Chosen()
        {
            Illness depresion = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(depresion);

            Psychologist psychologistChosen = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 01),
                ScheduleDays = new List<Schedule>() { },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologistChosen);
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(depresion, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologistChosen.Id);
        }

        [TestMethod]
        public void GetByIllnessAndDate_TwoAvailableExpertsOneWithoutSchedule_OlderChosen()
        {
            Illness depresion = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(depresion);

            Appointment appointment = new Appointment()
            {
                Id = 1,
                Date = new DateTime(2021, 04, 28)
            };
            this.context.Add(appointment);

            Psychologist psychologistChosen = new Psychologist()
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
                        },
                        Date = appointment.Date
                    }
                },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologistChosen);

            Psychologist psychologistNotChosen = new Psychologist()
            {
                Id = 2,
                FirstName = "Juan2",
                LastName = "Sartori2",
                Address = "Calle 12342",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 02),
                ScheduleDays = new List<Schedule>() { },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologistNotChosen);
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(depresion, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologistChosen.Id);
        }

        [TestMethod]
        public void GetByIllnessAndDate_ExpertWithLastScheduledDateBeforeUntilDate_Chosen()
        {
            Illness depresion = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(depresion);

            Appointment earlierAppointment = new Appointment()
            {
                Id = 1,
                Date = new DateTime(2021, 04, 27)
            };
            this.context.Add(earlierAppointment);

            Appointment laterAppointment = new Appointment()
            {
                Id = 2,
                Date = new DateTime(2021, 04, 28)
            };

            this.context.Add(earlierAppointment);
            Psychologist psychologistChosen = new Psychologist()
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
                           earlierAppointment
                        },
                        Date = earlierAppointment.Date
                    }
                },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologistChosen);

            Psychologist psychologistNotChosen = new Psychologist()
            {
                Id = 2,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "Calle 12342",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 02),
                ScheduleDays = new List<Schedule>()
                {
                    new Schedule()
                    {
                        Appointments = new List<Appointment>() {
                           laterAppointment
                        },
                        Date = laterAppointment.Date
                    }
                },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologistNotChosen);
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(depresion, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologistChosen.Id);
        }

        [TestMethod]
        public void GetByIllnessAndDate_TwoAvailableExpertsWithSameSchedule_OlderChosen()
		{
            Illness depresion = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(depresion);

            Appointment appointment = new Appointment()
            {
                Id = 1,
                Date = new DateTime(2021, 04, 28)
            };
            this.context.Add(appointment);

            Psychologist psychologistChosen = new Psychologist()
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
                        },
                        Date = appointment.Date
                    }
                },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologistChosen);

            Psychologist psychologistNotChosen = new Psychologist()
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
                        },
                        Date = appointment.Date
                    }
                },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologistNotChosen);
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(depresion, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologistChosen.Id);
        }

        [TestMethod]
        public void GetByIllnessAndDate_OlderExpertHasFullSchedule_ExpertWithEmptyScheduleChosen()
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

            Psychologist expertWithFullSchedule = new Psychologist()
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
            this.context.Add(expertWithFullSchedule);

            Psychologist expertWithEmptySchedule = new Psychologist()
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
            this.context.Add(expertWithEmptySchedule);

            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(estres, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == expertWithEmptySchedule.Id);
        }

        [TestMethod]
        public void GetByIllnessAndDate_ExpertWithLastScheduledDateAfterUntilDate_AvailbleExpertChosen()
        {
            Illness depresion = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(depresion);

            Appointment earlierAppointment = new Appointment()
            {
                Id = 1,
                Date = new DateTime(2021, 04, 27)
            };
            this.context.Add(earlierAppointment);

            Appointment laterAppointment = new Appointment()
            {
                Id = 2,
                Date = new DateTime(2021, 04, 30)
            };
            this.context.Add(earlierAppointment);

            Psychologist psychologistNotChosen = new Psychologist()
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
                           laterAppointment
                        },
                        Date = laterAppointment.Date
                    }
                },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologistNotChosen);

            Psychologist psychologistChosen = new Psychologist()
            {
                Id = 2,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "Calle 12342",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 02),
                ScheduleDays = new List<Schedule>()
                {
                    new Schedule()
                    {
                        Appointments = new List<Appointment>() {
                           earlierAppointment
                        },
                        Date = earlierAppointment.Date
                    }
                },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologistChosen);
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(depresion, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologistChosen.Id);
        }

        [TestMethod]
        public void GetByIllnessAndDate_NoExpertExistsForIllness_OlderChosen()
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
                        },
                        Date = appointment.Date
                    }
                },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologist);

            Psychologist psychologistNotChosen = new Psychologist()
            {
                Id = 2,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "Calle 5",
                Format = Format.Remote,
                CreatedDate = new DateTime(2021, 01, 02),
                ScheduleDays = new List<Schedule>()
                {
                    new Schedule()
                    {
                        Appointments = new List<Appointment>() {
                           appointment
                        },
                        Date = appointment.Date
                    }
                },
                Illnesses = new List<Illness>()
                {
                    depresion
                }
            };
            this.context.Add(psychologistNotChosen);

            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(estres, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologist.Id);
        }

        [TestMethod]
        public void GetByIllnessAndDate_NoExpertPsychologistAvailableWithoutSchedule_Chosen()
        {
            Illness depresion = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(depresion);

            Psychologist psychologistChosen = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 01),
                ScheduleDays = new List<Schedule>() { },
                Illnesses = new List<Illness>() { }
            };
            this.context.Add(psychologistChosen);
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(depresion, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologistChosen.Id);
        }

        [TestMethod]
        public void GetByIllnessAndDate_NoExpertPsychologistAvailableWithLastScheduledDateBeforeUntilDate_Chosen()
        {
            Illness depresion = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(depresion);

            Appointment earlierAppointment = new Appointment()
            {
                Id = 1,
                Date = new DateTime(2021, 04, 27)
            };
            this.context.Add(earlierAppointment);

            Appointment laterAppointment = new Appointment()
            {
                Id = 2,
                Date = new DateTime(2021, 04, 28)
            };

            this.context.Add(earlierAppointment);
            Psychologist psychologistChosen = new Psychologist()
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
                           earlierAppointment
                        },
                        Date = earlierAppointment.Date
                    }
                },
                Illnesses = new List<Illness>() { }
            };
            this.context.Add(psychologistChosen);

            Psychologist psychologistNotChosen = new Psychologist()
            {
                Id = 2,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "Calle 12342",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 02),
                ScheduleDays = new List<Schedule>()
                {
                    new Schedule()
                    {
                        Appointments = new List<Appointment>() {
                           laterAppointment
                        },
                        Date = laterAppointment.Date
                    }
                },
                Illnesses = new List<Illness>() { }
            };
            this.context.Add(psychologistNotChosen);
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(depresion, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologistChosen.Id);
        }

        [TestMethod]
        public void GetByIllnessAndDate_NoExpertOlderPsychologistHasFullSchedule_PsychologistWithEmptyScheduleChosen()
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

            Psychologist psychologistWithFullSchedule = new Psychologist()
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
                Illnesses = new List<Illness>() { }
            };
            this.context.Add(psychologistWithFullSchedule);

            Psychologist pychologistWithEmptySchedule = new Psychologist()
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 02),
                ScheduleDays = new List<Schedule>(),
                Illnesses = new List<Illness>() { }
            };
            this.context.Add(pychologistWithEmptySchedule);

            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(estres, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == pychologistWithEmptySchedule.Id);
        }

        [TestMethod]
        public void GetByIllnessAndDate_NoExpertPsychologistWithLastScheduledDateAfterUntilDate_AvailblePsychologistChosen()
        {
            Illness depresion = new Illness()
            {
                Id = 1,
                Name = "Depresion"
            };
            this.context.Add(depresion);

            Appointment earlierAppointment = new Appointment()
            {
                Id = 1,
                Date = new DateTime(2021, 04, 27)
            };
            this.context.Add(earlierAppointment);

            Appointment laterAppointment = new Appointment()
            {
                Id = 2,
                Date = new DateTime(2021, 04, 30)
            };
            this.context.Add(earlierAppointment);

            Psychologist psychologistNotChosen = new Psychologist()
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
                           laterAppointment
                        },
                        Date = laterAppointment.Date
                    }
                },
                Illnesses = new List<Illness>() { }
            };
            this.context.Add(psychologistNotChosen);

            Psychologist psychologistChosen = new Psychologist()
            {
                Id = 2,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "Calle 12342",
                Format = Format.OnSite,
                CreatedDate = new DateTime(2021, 01, 02),
                ScheduleDays = new List<Schedule>()
                {
                    new Schedule()
                    {
                        Appointments = new List<Appointment>() {
                           earlierAppointment
                        },
                        Date = earlierAppointment.Date
                    }
                },
                Illnesses = new List<Illness>() { }
            };
            this.context.Add(psychologistChosen);
            this.context.SaveChanges();

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtained = repository.Get(depresion, new DateTime(2021, 04, 28), 5);

            Assert.IsTrue(obtained.Id == psychologistChosen.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void GetWithIllness_NoPsychologists_ExceptionThrown()
        {
            Illness estres = new Illness()
            {
                Id = 1,
                Name = "Estres"
            };
            this.context.Add(estres);

            PsychologistRepository repository = new PsychologistRepository(this.context);

            Psychologist obtainedPsychologist = repository.Get(estres, new DateTime(2021, 10, 10), 5);

            Assert.IsNull(obtainedPsychologist);
        }

        [TestMethod]
        public void Update_DataIsCorrect_Updated()
        {
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(psychologist);

            Appointment appointment = new Appointment()
            {
                Id = 1,
                Psychologist = psychologist
            };
            this.context.Add(appointment);
            this.context.SaveChanges();

            Schedule schedule = new Schedule()
            {
                Id = 1,
                Appointments = new List<Appointment>()
                    {
                        appointment
                    },
                Psychologist = psychologist
            };
            this.context.Add(schedule);

            psychologist.ScheduleDays = new List<Schedule>()
            {
                schedule
            };
            repository.Update(psychologist);
            Psychologist obtained = repository.Get(psychologist.Id);

            Assert.AreEqual(psychologist, obtained);
        }

        [TestMethod]
        public void Update_UpdateAllProperties_Updated()
        {
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(psychologist);

            Appointment appointment = new Appointment()
            {
                Id = 1,
                Psychologist = psychologist
            };
            this.context.Add(appointment);
            this.context.SaveChanges();

            Schedule schedule = new Schedule()
            {
                Id = 1,
                Appointments = new List<Appointment>()
                    {
                        appointment
                    },
                Psychologist = psychologist
            };
            this.context.Add(schedule);

            psychologist.ScheduleDays = new List<Schedule>()
            {
                schedule
            };

            Illness newIllness = new Illness()
            {
                Id = 1,
                Name = "estres"
            };
            this.context.Add(newIllness);
            this.context.SaveChanges();

            psychologist.Address = "address";
            psychologist.FirstName = "first name";
            psychologist.Format = Format.Remote;
            psychologist.Illnesses = new List<Illness>()
            {
                newIllness
            };
            psychologist.LastName = "last name";

            repository.Update(psychologist);
            Psychologist obtained = repository.Get(psychologist.Id);

            Assert.AreEqual(psychologist, obtained);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Delete_PsychologistFound_Deleted()
        {
            Psychologist psychologistToDelete = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(psychologistToDelete);
            repository.Delete(psychologistToDelete.Id);

            Psychologist obtainedPsychologist = repository.Get(psychologistToDelete.Id);

            Assert.IsNull(obtainedPsychologist);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Update_NoAddress_ExceptionThrown()
        {
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                Address = "Address of Sartori",
                FirstName = "Juan",
                LastName = "Sartori",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(psychologist);

            psychologist.Address = string.Empty;
            repository.Update(psychologist);
            
            Psychologist obtainedPsychologist = repository.Get(psychologist.Id);

            Assert.AreNotEqual(psychologist.Address, obtainedPsychologist.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Update_NoFirstName_ExceptionThrown()
        {
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                Address = "Address of Sartori",
                FirstName = "Juan",
                LastName = "Sartori",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(psychologist);

            psychologist.FirstName = string.Empty;
            repository.Update(psychologist);
            
            Psychologist obtainedPsychologist = repository.Get(psychologist.Id);

            Assert.AreNotEqual(psychologist.FirstName, obtainedPsychologist.FirstName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidInputException))]
        public void Update_NoLastName_ExceptionThrown()
        {
            Psychologist psychologist = new Psychologist()
            {
                Id = 1,
                Address = "Address of Sartori",
                FirstName = "Juan",
                LastName = "Sartori",
                Format = Format.OnSite,
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            PsychologistRepository repository = new PsychologistRepository(this.context);
            repository.Add(psychologist);

            psychologist.LastName = string.Empty;
            repository.Update(psychologist);
            
            Psychologist obtainedPsychologist = repository.Get(psychologist.Id);

            Assert.AreNotEqual(psychologist.LastName, obtainedPsychologist.LastName);
        }
    }
}
