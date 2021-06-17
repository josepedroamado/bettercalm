using BLInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebAPI.Controllers;

namespace WebAPI.Test
{
    [TestClass]
    public class ImportersControllerTest
    {
        [TestMethod]
        public void Post_JSON_OK()
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "importTest.json");
            string fileType = "JSON";

            ImporterInputModel inputModel = new ImporterInputModel()
            {
                FilePath = filePath,
                Type = fileType
            };

            Mock<IImporterLogic> mock = new Mock<IImporterLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Import(fileType, filePath));
            ImportersController controller = new ImportersController(mock.Object);
            IActionResult result = controller.Post(inputModel);
            Assert.IsTrue(result is NoContentResult);
        }

        [TestMethod]
        public void Post_XML_OK()
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "importTest.xml");
            string fileType = "XML";

            ImporterInputModel inputModel = new ImporterInputModel()
            {
                FilePath = filePath,
                Type = fileType
            };

            Mock<IImporterLogic> mock = new Mock<IImporterLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Import(fileType, filePath));
            ImportersController controller = new ImportersController(mock.Object);
            IActionResult result = controller.Post(inputModel);
            Assert.IsTrue(result is NoContentResult);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Post_JSON_NotFound()
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "notfound.json");
            string fileType = "JSON";
            int expectedStatusCode = 400;

            ImporterInputModel inputModel = new ImporterInputModel()
            {
                FilePath = filePath,
                Type = fileType
            };

            Mock<IImporterLogic> mock = new Mock<IImporterLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Import(fileType, filePath)).Throws(new FileNotFoundException());
            ImportersController controller = new ImportersController(mock.Object);
            IActionResult result = controller.Post(inputModel);
            StatusCodeResult contentResult = result as StatusCodeResult;
            Assert.AreEqual(expectedStatusCode, contentResult.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void Post_XML_NotFound()
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "notfound.xml");
            string fileType = "XML";
            int expectedStatusCode = 400;

            ImporterInputModel inputModel = new ImporterInputModel()
            {
                FilePath = filePath,
                Type = fileType
            };

            Mock<IImporterLogic> mock = new Mock<IImporterLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Import(fileType, filePath)).Throws(new FileNotFoundException());
            ImportersController controller = new ImportersController(mock.Object);
            IActionResult result = controller.Post(inputModel);
            StatusCodeResult contentResult = result as StatusCodeResult;
            Assert.AreEqual(expectedStatusCode, contentResult.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ImporterNotFoundException))]
        public void Post_InvalidImporter_ImporterNotFound()
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(basePath, "importTest.csv");
            string fileType = "CSV";
            int expectedStatusCode = 500;

            ImporterInputModel inputModel = new ImporterInputModel()
            {
                FilePath = filePath,
                Type = fileType
            };

            Mock<IImporterLogic> mock = new Mock<IImporterLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Import(fileType, filePath)).Throws(new ImporterNotFoundException(fileType, ""));
            ImportersController controller = new ImportersController(mock.Object);
            IActionResult result = controller.Post(inputModel);
            StatusCodeResult contentResult = result as StatusCodeResult;
            Assert.AreEqual(expectedStatusCode, contentResult.StatusCode);
        }

        [TestMethod]
        public void GetTypes_ExistTypes_Fetched()
		{
            List<string> expectedTypes = new List<string>()
            {
                "JSON",
                "XML"
            };


            Mock<IImporterLogic> mock = new Mock<IImporterLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetTypes()).Returns(expectedTypes);
            ImportersController controller = new ImportersController(mock.Object);

            IActionResult result = controller.GetTypes();
            OkObjectResult objectResult = result as OkObjectResult;
            ImporterTypesModel obtainedOutput = objectResult.Value as ImporterTypesModel;

            Assert.IsTrue(expectedTypes.SequenceEqual(obtainedOutput.Types));
        }
    }
}
