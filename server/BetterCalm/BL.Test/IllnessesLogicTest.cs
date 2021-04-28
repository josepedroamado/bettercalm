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
        public void GetAllOk()
        {
            List<Illness> expectedIllnesses = GetAllOkExpected();
            Mock<IIllnessRepository> illnessRepositoryMock = new Mock<IIllnessRepository>(MockBehavior.Strict);
            illnessRepositoryMock.Setup(m => m.GetAll()).Returns(expectedIllnesses);

            IllnessLogic illnessLogic = new IllnessLogic(illnessRepositoryMock.Object);

            IEnumerable<Illness> obtainedIllnesses = illnessLogic.GetIllnesses();

            illnessRepositoryMock.VerifyAll();
            Assert.IsTrue(obtainedIllnesses.SequenceEqual(expectedIllnesses));
        }

        private List<Illness> GetAllOkExpected()
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
