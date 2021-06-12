using BLInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Controllers;

namespace WebAPI.Test
{
    [TestClass]
    public class PsychologistsControllerTest
    {
        [TestMethod]
        public void Get_PsychologistsExist_Fetched()
        {
            List<Psychologist> expectedPsychologists = GetAllExpectedPsychologists();
            List<PsychologistModel> expectedPsychologistModels = new List<PsychologistModel>();
            foreach (Psychologist psychologist in expectedPsychologists)
            {
                expectedPsychologistModels.Add(new PsychologistModel(psychologist));
            }

            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(m => m.GetAll()).Returns(expectedPsychologists);

            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);

            IActionResult result = psychologistsController.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<PsychologistModel> obtainedPsychologistModels = objectResult.Value as IEnumerable<PsychologistModel>;

            psychologistLogicMock.VerifyAll();
            Assert.IsTrue(expectedPsychologistModels.SequenceEqual(obtainedPsychologistModels));
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void Get_NoPsychologistsExist_ExpectedException()
        {
            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(m => m.GetAll()).Throws(new CollectionEmptyException("Psychologists"));

            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);

            IActionResult result = psychologistsController.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<PsychologistModel> obtainedPsychologistModels = objectResult.Value as IEnumerable<PsychologistModel>;

            psychologistLogicMock.VerifyAll();
            Assert.IsNull(obtainedPsychologistModels);
        }

        private List<Psychologist> GetAllExpectedPsychologists()
        {
            PsychologistRate thousand = new PsychologistRate()
            {
                Id = 1,
                HourlyRate = 1000
            };
            Psychologist firstPsychologist = new Psychologist()
            {
                Id = 1,
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                Rate = thousand
            };
            PsychologistRate twoThousand = new PsychologistRate()
            {
                Id = 2,
                HourlyRate = 2000
            };
            Psychologist secondPsychologist = new Psychologist()
            {
                Id = 2,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "14th Street",
                Format = Format.Remote,
                Rate = twoThousand
            };
            List<Psychologist> expectedPsychologists = new List<Psychologist>() { firstPsychologist, secondPsychologist };
            return expectedPsychologists;
        }

        [TestMethod]
        public void GetById_PsychologistFound_Fetched()
        {
            PsychologistRate thousand = new PsychologistRate()
            {
                Id = 1,
                HourlyRate = 1000
            };
            Psychologist expectedPsychologist = new Psychologist()
            {
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                Rate = thousand
            };

            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(m => m.Get(expectedPsychologist.Id)).Returns(expectedPsychologist);

            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);

            IActionResult result = psychologistsController.Get(expectedPsychologist.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            PsychologistModel obtainedPsychologistModel = (objectResult.Value as PsychologistModel);

            psychologistLogicMock.VerifyAll();
            Assert.AreEqual(new PsychologistModel(expectedPsychologist), obtainedPsychologistModel);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetById_PsychologistNotFound_ExceptionThrown()
        {
            PsychologistModel expectedPsychologistModel = new PsychologistModel()
            {
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = "OnSite",
                Rate = 1000
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
        public void Post_DataIsCorrect_Created()
        {
            PsychologistRate thousand = new PsychologistRate()
            {
                Id = 1,
                HourlyRate = 1000
            };
            Psychologist expectedPsychologist = new Psychologist()
            {
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                Rate = thousand
            };
            PsychologistModel expectedPsychologistModel = new PsychologistModel(expectedPsychologist);

            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(m => m.Add(It.IsAny<Psychologist>()));
            psychologistLogicMock.Setup(m => m.Get(expectedPsychologist.Id)).Returns(expectedPsychologist);
            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);
            psychologistsController.Post(expectedPsychologistModel);

            IActionResult result = psychologistsController.Get(expectedPsychologist.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            PsychologistModel obtainedPsychologistModel = (objectResult.Value as PsychologistModel);

            Assert.AreEqual(expectedPsychologistModel, obtainedPsychologistModel);  
        }

        [TestMethod]
        [ExpectedException(typeof(AlreadyExistsException))]
        public void Post_AlreadyExists_ExceptionThrown()
        {
            PsychologistModel expectedPsychologistModel = new PsychologistModel()
            {
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = "OnSite",
                Rate = 1000
            };

            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(p => p.Add(It.IsAny<Psychologist>())).Throws(new AlreadyExistsException(expectedPsychologistModel.Id.ToString()));

            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);
            psychologistsController.Post(expectedPsychologistModel);

            IActionResult result = psychologistsController.Get(expectedPsychologistModel.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            PsychologistModel obtainedPsychologistModel = (objectResult.Value as PsychologistModel);

            Assert.AreEqual(expectedPsychologistModel, obtainedPsychologistModel);
        }

        [TestMethod]
        public void Patch_DataIsCorrect_Updated()
        {
            PsychologistRate thousand = new PsychologistRate()
            { 
                Id = 1, 
                HourlyRate = 1000 
            };
            Psychologist originalPsychologist = new Psychologist()
            {
                FirstName = "Juan",
                LastName = "Sartori",
                Address = "Calle 1234",
                Format = Format.OnSite,
                Rate = thousand
            };
            PsychologistModel originalPsychologistModel = new PsychologistModel(originalPsychologist);

            PsychologistModel newPsychologistModel = new PsychologistModel()
            {
                FirstName = "Orestes",
                LastName = "Fiandra",
                Address = "General Paz 1234",
                Format = "Remote",
                Rate = 2000
            };

            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(m => m.Update(It.IsAny<Psychologist>()));
            psychologistLogicMock.Setup(m => m.Get(originalPsychologist.Id)).Returns(originalPsychologist);
            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);
            psychologistsController.Patch(originalPsychologistModel);

            psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(m => m.Get(originalPsychologistModel.Id)).Returns(newPsychologistModel.ToEntity());

            IActionResult result = psychologistsController.Get(originalPsychologist.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            PsychologistModel obtainedPsychologistModel = (objectResult.Value as PsychologistModel);

            Assert.AreEqual(newPsychologistModel, obtainedPsychologistModel);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Delete_PsychologistFound_Deleted()
        {
            PsychologistModel psychologistToDelete = new PsychologistModel()
            {
                FirstName = "Hannibal",
                LastName = "Lecter",
                Address = "Calle 1234",
                Format = "OnSite",
                Rate = 1000
            };
            PsychologistDeleteModel deleteModel = new PsychologistDeleteModel()
            {
                Id = psychologistToDelete.Id
            };
            Mock<IPsychologistLogic> psychologistLogicMock = new Mock<IPsychologistLogic>(MockBehavior.Strict);
            psychologistLogicMock.Setup(m => m.Delete(psychologistToDelete.Id));
            psychologistLogicMock.Setup(m => m.Get(psychologistToDelete.Id)).Returns(psychologistToDelete.ToEntity());

            PsychologistsController psychologistsController = new PsychologistsController(psychologistLogicMock.Object);
            psychologistsController.Delete(deleteModel);

            psychologistLogicMock.Setup(m => m.Get(psychologistToDelete.Id)).Throws(new NotFoundException(psychologistToDelete.Id.ToString()));

            IActionResult result = psychologistsController.Get(psychologistToDelete.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            PsychologistModel obtainedPsychologistModel = (objectResult.Value as PsychologistModel);

            Assert.IsNull(obtainedPsychologistModel);
        }
    }
}
