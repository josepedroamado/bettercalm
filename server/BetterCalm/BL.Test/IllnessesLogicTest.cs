using DataAccessInterfaces;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace BL.Test
{
    [TestClass]
    public class IllnessesLogicTest
    {
        [TestMethod]
        public void GetAll_IllnessesExist_IllnessesFetched()
        {
            List<Illness> expectedIllnesses = GetExpectedIllnesses();
            Mock<IIllnessRepository> illnessRepositoryMock = new Mock<IIllnessRepository>(MockBehavior.Strict);
            illnessRepositoryMock.Setup(m => m.GetAll()).Returns(expectedIllnesses);

            IllnessLogic illnessLogic = new IllnessLogic(illnessRepositoryMock.Object);

            IEnumerable<Illness> obtainedIllnesses = illnessLogic.GetIllnesses();

            illnessRepositoryMock.VerifyAll();
            Assert.IsTrue(obtainedIllnesses.SequenceEqual(expectedIllnesses));
        }

        private List<Illness> GetExpectedIllnesses()
        {
            Illness depression = new Illness()
            {
                Id = 1,
                Name = "Depression"
            };

            Illness stress = new Illness()
            {
                Id = 2,
                Name = "Stress"
            };
            return new List<Illness>() { depression, stress };
        }
    }
}
