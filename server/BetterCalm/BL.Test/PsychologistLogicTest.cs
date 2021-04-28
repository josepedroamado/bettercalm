using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BL.Test
{
    [TestClass]
    public class PsychologistLogicTest
    {
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

            Mock<IPsychologistRepository> psychologistRepositoryMock = new Mock<IPsychologistRepository>(MockBehavior.Strict);
            psychologistRepositoryMock.Setup(m => m.Get(expectedPsychologist.Id)).Returns(expectedPsychologist);

            PsychologistLogic psychologistLogic = new PsychologistLogic(psychologistRepositoryMock.Object);

            Psychologist obtainedPsychologist = psychologistLogic.Get(expectedPsychologist.Id);

            Assert.AreEqual(expectedPsychologist, obtainedPsychologist);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetNotFound()
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

            Mock<IPsychologistRepository> psychologistRepositoryMock = new Mock<IPsychologistRepository>(MockBehavior.Strict);
            psychologistRepositoryMock.Setup(m => m.Get(expectedPsychologist.Id)).Throws(new NotFoundException(expectedPsychologist.Id.ToString()));

            PsychologistLogic psychologistLogic = new PsychologistLogic(psychologistRepositoryMock.Object);

            Psychologist obtainedPsychologist = psychologistLogic.Get(expectedPsychologist.Id);

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

            Mock<IPsychologistRepository> psychologistRepositoryMock = new Mock<IPsychologistRepository>(MockBehavior.Strict);
            psychologistRepositoryMock.Setup(m => m.Add(expectedPsychologist));
            psychologistRepositoryMock.Setup(m => m.Get(expectedPsychologist.Id)).Returns(expectedPsychologist);

            PsychologistLogic psychologistLogic = new PsychologistLogic(psychologistRepositoryMock.Object);
            psychologistLogic.Add(expectedPsychologist);

            Psychologist obtainedPsychologist = psychologistLogic.Get(expectedPsychologist.Id);

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

            Mock<IPsychologistRepository> psychologistRepositoryMock = new Mock<IPsychologistRepository>(MockBehavior.Strict);
            psychologistRepositoryMock.Setup(m => m.Add(expectedPsychologist)).Throws(new AlreadyExistsException(expectedPsychologist.Id.ToString()));
            psychologistRepositoryMock.Setup(m => m.Get(expectedPsychologist.Id)).Returns(expectedPsychologist);

            PsychologistLogic psychologistLogic = new PsychologistLogic(psychologistRepositoryMock.Object);
            psychologistLogic.Add(expectedPsychologist);

            Psychologist obtainedPsychologist = psychologistLogic.Get(expectedPsychologist.Id);

            Assert.AreEqual(expectedPsychologist, obtainedPsychologist);
        }
    }
}
