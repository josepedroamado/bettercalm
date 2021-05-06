using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
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
        public void Get_IllnessesExist_Fetched()
        {
            List<Illness> expectedIllnesses = GetAllExpected();
            List<IllnessModel> expectedIllnessesConvertedToModel = expectedIllnesses.Select(illness => new IllnessModel(illness)).ToList();

            Mock<IIllnessLogic> mock = new Mock<IIllnessLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetIllnesses()).Returns(expectedIllnesses);
            IllnessesController controller = new IllnessesController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<IllnessModel> obtainedIllnesses = objectResult.Value as IEnumerable<IllnessModel>;

            mock.VerifyAll();
            Assert.IsTrue(expectedIllnessesConvertedToModel.SequenceEqual(obtainedIllnesses));
        }

        private List<Illness> GetAllExpected()
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
