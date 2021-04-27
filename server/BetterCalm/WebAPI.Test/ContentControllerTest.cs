using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Controllers;
using System.Linq;

namespace WebAPI.Test
{
	[TestClass]
	public class ContentControllerTest
	{
		[TestMethod]
		public void GetContentsOk()
		{
            List<Content> expectedContents = GetExpectedContents();

            Mock<IContentPlayer> mock = new Mock<IContentPlayer>(MockBehavior.Strict);
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
    }
}
