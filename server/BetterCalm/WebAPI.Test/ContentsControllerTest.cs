using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using WebAPI.Controllers;
using System.Linq;
using Domain.Exceptions;

namespace WebAPI.Test
{
    [TestClass]
	public class ContentsControllerTest
	{
		[TestMethod]
		public void GetContentsOk()
		{
            List<Content> expectedContents = GetExpectedContents();

            Mock<IContentLogic> mock = new Mock<IContentLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetContents()).Returns(expectedContents);
            ContentsController controller = new ContentsController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<ContentBasicInfo> obtainedContents = objectResult.Value as IEnumerable<ContentBasicInfo>;

            mock.VerifyAll();
            CollectionAssert.AreEqual(expectedContents.
                Select(content => new ContentBasicInfo(content)).
                ToList(),
                obtainedContents.ToList(),
                new ContentBasicInfoComparer());

		}

        private static List<Content> GetExpectedContents()
        {
            return new List<Content>()
            {
                new Content()
                {
                    ArtistName = "Bon Jovi",
                    Categories = new List<Category>(){
                        new Category()
                        {
                            Id = 1,
                            Name = "Rock"
                        }
                    },
                    Id = 1,
                    ContentLength = new TimeSpan(0, 2, 30),
                    Name = "It's My Life",
                    ImageUrl = "http://www.images.com/image.jpg"
                },
                new Content()
                {
                    ArtistName = "Celia Cruz",
                    Categories = new List<Category>(){
                        new Category()
                        {
                            Id = 2,
                            Name = "Tropical"
                        }
                    },
                    Id = 2,
                    ContentLength = new TimeSpan(0, 2, 30),
                    Name = "La vida es un carnaval",
                    ImageUrl = "http://www.images.com/image2.jpg"
                }
            };
        }

        [TestMethod]
        public void GetContentOk()
        {
            Content expectedContent = new Content()
            {
                ArtistName = "Bon Jovi",
                Categories = new List<Category>(){
                        new Category()
                        {
                            Id = 1,
                            Name = "Rock"
                        }
                    },
                Id = 1,
                ContentLength = new TimeSpan(0, 2, 30),
                Name = "It's My Life",
                ImageUrl = "http://www.images.com/image.jpg"
            };

            Mock<IContentLogic> mock = new Mock<IContentLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetContent(expectedContent.Id)).Returns(expectedContent);
            ContentsController controller = new ContentsController(mock.Object);

            IActionResult result = controller.Get(expectedContent.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            ContentBasicInfo obtainedContent = objectResult.Value as ContentBasicInfo;

            mock.VerifyAll();
            Assert.IsTrue((new ContentBasicInfoComparer()).
               Compare(new ContentBasicInfo(expectedContent), obtainedContent) == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetContentNotFound()
        {
            int toGetContentId = 1;

            Mock<IContentLogic> mock = new Mock<IContentLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetContent(toGetContentId)).Throws(new NotFoundException(toGetContentId.ToString()));
            ContentsController controller = new ContentsController(mock.Object);

            IActionResult result = controller.Get(toGetContentId);
            OkObjectResult objectResult = result as OkObjectResult;
            ContentBasicInfo obtainedContent = objectResult.Value as ContentBasicInfo;

            mock.VerifyAll();
            Assert.IsNull(obtainedContent);
        }
    }
}
