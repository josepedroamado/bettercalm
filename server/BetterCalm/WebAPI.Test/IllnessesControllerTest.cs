using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Controllers;

namespace WebAPI.Test
{
    [TestClass]
    public class IllnessesControllerTest
    {
        [TestMethod]
        public void GetOk()
        {
            List<Illness> expectedIllnesses = GetAllOkExpected();

            Mock<IIllnessLogic> mock = new Mock<IIllnessLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetIllnesses()).Returns(expectedIllnesses);
            IllnessesController controller = new IllnessesController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<Illness> obtainedIllnesses = objectResult.Value as IEnumerable<Illness>;

            mock.VerifyAll();
            Assert.IsTrue(expectedIllnesses.SequenceEqual(obtainedIllnesses));
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
