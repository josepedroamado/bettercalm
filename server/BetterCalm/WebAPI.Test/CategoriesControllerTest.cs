using BLInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAPI.Controllers;

namespace WebAPI.Test
{
    [TestClass]
    public class CategoriesControllerTest
    {
        [TestMethod]
        public void GetAllCategories()
        {
			List<Category> expectedCategories = GetCategoriesOkExpected();

			Mock<ICategoryLogic> mock = new Mock<ICategoryLogic>(MockBehavior.Strict);
			mock.Setup(m => m.GetCategories()).Returns(expectedCategories);
			CategoriesController controller = new CategoriesController(mock.Object);

			IActionResult result = controller.Get();
			OkObjectResult objectResult = result as OkObjectResult;
			IEnumerable<Category> obtainedCategories = objectResult.Value as IEnumerable<Category>;

			mock.VerifyAll();
			Assert.IsTrue(expectedCategories.SequenceEqual(obtainedCategories));
		}

        private List<Category> GetCategoriesOkExpected()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Sleep",
                    PlayLists = new List<Playlist>()
                    {
                        new Playlist()
                        {
                            Id = 1,
                            Name = "Nature ambiences",
                            Description = "The best nature ambiences to put you to sleep",
                            ImageUrl = "http://myimageurl.com/image.jpg",
                            Contents = new List<Content>()
                            {
                                new Content()
                                {
                                    Id = 1,
                                    ArtistName = "AmbienceOne",
                                    Name = "Rain",
                                    ImageUrl = "http://myimageurl.com/image.jpg",
                                    AudioUrl = "http://www.audios.com/audio.mp3"
                                }
                            }
                        }
                    },
                    Contents = new List<Content>()
                    {
                        new Content()
                        {
                            Id = 2,
                            ArtistName = "AmbienceOne",
                            Name = "Campfire",
                            ImageUrl = "http://myimageurl.com/image.jpg",
                            AudioUrl = "http://www.audios.com/audio.mp3"
                        }
                    }
                },
                new Category()
                {
                    Id = 2,
                    Name = "Mediate",
                    PlayLists = new List<Playlist>()
                    {
                        new Playlist()
                        {
                            Id = 2,
                            Name = "Mantras",
                            Description = "The best mantras to meditate",
                            ImageUrl = "http://myimageurl.com/image.jpg",
                            Contents = new List<Content>()
                            {
                                new Content()
                                {
                                    Id = 3,
                                    ArtistName = "Mantrastic",
                                    Name = "Indian Mantra",
                                    ImageUrl = "http://myimageurl.com/image.jpg",
                                    AudioUrl = "http://www.audios.com/audio.mp3"
                                }
                            }
                        }
                    },
                    Contents = new List<Content>()
                    {

                    }
                }
            };
        }

        [TestMethod]
        public void GetCategoryById()
        {
            Category expectedCategory = GetCategoryOkExpected();
            Mock<ICategoryLogic> mock = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetCategory(expectedCategory.Id)).Returns(expectedCategory);
            CategoriesController controller = new CategoriesController(mock.Object);

            IActionResult result = controller.Get(expectedCategory.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            Category obtainedCategory = objectResult.Value as Category;

            Assert.AreEqual(expectedCategory, obtainedCategory);
        }

        private Category GetCategoryOkExpected()
        {
            return new Category()
            {
                Id = 1,
                Name = "Sleep",
                PlayLists = new List<Playlist>()
                    {
                        new Playlist()
                        {
                            Id = 1,
                            Name = "Nature ambiences",
                            Description = "The best nature ambiences to put you to sleep",
                            ImageUrl = "http://myimageurl.com/image.jpg",
                            Contents = new List<Content>()
                            {
                                new Content()
                                {
                                    Id = 1,
                                    ArtistName = "AmbienceOne",
                                    Name = "Rain",
                                    ImageUrl = "http://myimageurl.com/image.jpg",
                                    AudioUrl = "http://www.audios.com/audio.mp3"
                                }
                            }
                        }
                    },
                Contents = new List<Content>()
                    {
                        new Content()
                        {
                            Id = 2,
                            ArtistName = "AmbienceOne",
                            Name = "Campfire",
                            ImageUrl = "http://myimageurl.com/image.jpg",
                            AudioUrl = "http://www.audios.com/audio.mp3"
                        }
                    }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetCategoryByIdNotFound()
        {
            int expectedCategoryId = 1;

            Mock<ICategoryLogic> mock = new Mock<ICategoryLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetCategory(expectedCategoryId)).Throws(new NotFoundException(expectedCategoryId.ToString()));
            CategoriesController controller = new CategoriesController(mock.Object);

            IActionResult result = controller.Get(expectedCategoryId);

            mock.VerifyAll();
            Assert.IsTrue(result is NotFoundObjectResult);
        }
    }
}
