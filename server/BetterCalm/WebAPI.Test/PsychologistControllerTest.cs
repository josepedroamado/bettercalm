using BLInterfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using WebAPI.Controllers;

namespace WebAPI.Test
{
    [TestClass]
    public class PsychologistControllerTest
    {
        [TestMethod]
        public void GetOk()
        {
            PsychologistModel expectedPsychologistModel = new PsychologistModel()
            {
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = 0
            };

            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(m => m.Get(expectedPsychologistModel.Id)).Returns(expectedPsychologistModel.ToEntity());

            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);

            IActionResult result = psychologistsController.Get(expectedPsychologistModel.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            PsychologistModel obtainedPsychologistModel = (objectResult.Value as PsychologistModel);

            psychologistLogicMock.VerifyAll();
            Assert.AreEqual(expectedPsychologistModel, obtainedPsychologistModel);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetNotFound()
        {
            PsychologistModel expectedPsychologistModel = new PsychologistModel()
            {
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = 0
            };

            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(m => m.Get(expectedPsychologistModel.Id)).Throws(new NotFoundException(expectedPsychologistModel.Id.ToString()));

            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);

            IActionResult result = psychologistsController.Get(expectedPsychologistModel.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            PsychologistModel obtainedPsychologistModel = (objectResult.Value as PsychologistModel);

            psychologistLogicMock.VerifyAll();
            Assert.IsNull(obtainedPsychologistModel);
        }

        [TestMethod]
        public void PostOk()
        {
            PsychologistModel expectedPsychologistModel = new PsychologistModel()
            {
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = 0
            };

            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(m => m.Add(expectedPsychologistModel.ToEntity()));
            psychologistLogicMock.Setup(m => m.Get(expectedPsychologistModel.Id)).Returns(expectedPsychologistModel.ToEntity());
            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);
            psychologistsController.Post(expectedPsychologistModel);

            IActionResult result = psychologistsController.Get(expectedPsychologistModel.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            PsychologistModel obtainedPsychologistModel = (objectResult.Value as PsychologistModel);

            Assert.AreEqual(expectedPsychologistModel, obtainedPsychologistModel);  
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsException))]
        public void PostAlreadyExists()
        {
            PsychologistModel expectedPsychologistModel = new PsychologistModel()
            {
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = 0
            };

            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(p => p.Add(expectedPsychologistModel.ToEntity())).Throws(new AlreadyExistsException(expectedPsychologistModel.Id.ToString()));

            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);
            psychologistsController.Post(expectedPsychologistModel);

            IActionResult result = psychologistsController.Get(expectedPsychologistModel.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            PsychologistModel obtainedPsychologistModel = (objectResult.Value as PsychologistModel);

            Assert.AreEqual(expectedPsychologistModel, obtainedPsychologistModel);
        }
    }
}
