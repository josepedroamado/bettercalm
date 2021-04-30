using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

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

            PsychologistLogic psychologistLogic = new PsychologistLogic(psychologistRepositoryMock.Object, It.IsAny<IIllnessRepository>());

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

            PsychologistLogic psychologistLogic = new PsychologistLogic(psychologistRepositoryMock.Object, It.IsAny<IIllnessRepository>());

            Psychologist obtainedPsychologist = psychologistLogic.Get(expectedPsychologist.Id);

            Assert.IsNull(obtainedPsychologist);
        }

        [TestMethod]
        public void AddOk()
        {
            Illness stress = new Illness { Id = 1, Name = "Stress" };
            Illness depression = new Illness { Id = 2, Name = "Depression" };
            Psychologist expectedPsychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                Illnesses = new List<Illness>(){ stress, depression },
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            Mock<IPsychologistRepository> psychologistRepositoryMock = new Mock<IPsychologistRepository>(MockBehavior.Strict);
            psychologistRepositoryMock.Setup(m => m.Add(expectedPsychologist));
            psychologistRepositoryMock.Setup(m => m.Get(expectedPsychologist.Id)).Returns(expectedPsychologist);

            Mock<IIllnessRepository> illnessRepositoryMock = new Mock<IIllnessRepository>(MockBehavior.Strict);
            illnessRepositoryMock.Setup(m => m.Get(stress.Id)).Returns(stress);
            illnessRepositoryMock.Setup(m => m.Get(depression.Id)).Returns(depression);

            PsychologistLogic psychologistLogic = new PsychologistLogic(psychologistRepositoryMock.Object, illnessRepositoryMock.Object);
            psychologistLogic.Add(expectedPsychologist);

            Psychologist obtainedPsychologist = psychologistLogic.Get(expectedPsychologist.Id);

            Assert.AreEqual(expectedPsychologist, obtainedPsychologist);
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsException))]
        public void AddAlreadyExists()
        {
            Illness stress = new Illness { Id = 1, Name = "Stress" };
            Illness depression = new Illness { Id = 2, Name = "Depression" };
            Psychologist expectedPsychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                Illnesses = new List<Illness>() { stress, depression },
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            Mock<IPsychologistRepository> psychologistRepositoryMock = new Mock<IPsychologistRepository>(MockBehavior.Strict);
            psychologistRepositoryMock.Setup(m => m.Add(expectedPsychologist)).Throws(new AlreadyExistsException(expectedPsychologist.Id.ToString()));
            psychologistRepositoryMock.Setup(m => m.Get(expectedPsychologist.Id)).Returns(expectedPsychologist);

            Mock<IIllnessRepository> illnessRepositoryMock = new Mock<IIllnessRepository>(MockBehavior.Strict);
            illnessRepositoryMock.Setup(m => m.Get(stress.Id)).Returns(stress);
            illnessRepositoryMock.Setup(m => m.Get(depression.Id)).Returns(depression);

            PsychologistLogic psychologistLogic = new PsychologistLogic(psychologistRepositoryMock.Object, illnessRepositoryMock.Object);
            psychologistLogic.Add(expectedPsychologist);

            Psychologist obtainedPsychologist = psychologistLogic.Get(expectedPsychologist.Id);

            Assert.AreEqual(expectedPsychologist, obtainedPsychologist);
        }

        [TestMethod]
        [ExpectedException(typeof(ExceedingNumberOfIllnessesException))]
        public void AddWithMoreThanThreeIllnessesFails()
        {
            Illness stress = new Illness { Id = 1, Name = "Stress" };
            Illness depression = new Illness { Id = 2, Name = "Depression" };
            Illness anxiety = new Illness { Id = 2, Name = "Anxiety" };
            Illness rage = new Illness { Id = 2, Name = "Rage" };
            Psychologist expectedPsychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                Illnesses = new List<Illness>() { stress, depression, anxiety, rage },
                CreatedDate = DateTime.Today.AddMonths(-3)
            };

            Mock<IPsychologistRepository> psychologistRepositoryMock = new Mock<IPsychologistRepository>(MockBehavior.Strict);
            psychologistRepositoryMock.Setup(m => m.Add(expectedPsychologist));
            psychologistRepositoryMock.Setup(m => m.Get(expectedPsychologist.Id)).Returns(expectedPsychologist);

            Mock<IIllnessRepository> illnessRepositoryMock = new Mock<IIllnessRepository>(MockBehavior.Strict);
            illnessRepositoryMock.Setup(m => m.Get(stress.Id)).Returns(stress);
            illnessRepositoryMock.Setup(m => m.Get(depression.Id)).Returns(depression);

            PsychologistLogic psychologistLogic = new PsychologistLogic(psychologistRepositoryMock.Object, illnessRepositoryMock.Object);
            psychologistLogic.Add(expectedPsychologist);

            Psychologist obtainedPsychologist = psychologistLogic.Get(expectedPsychologist.Id);

            Assert.IsNull(obtainedPsychologist);
        }
    }
}
