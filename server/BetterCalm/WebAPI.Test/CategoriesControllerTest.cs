using BLInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
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
			CategoriesController controller = new CategoriesController(mock.Object, It.IsAny<IContentLogic>(), It.IsAny<IPlaylistLogic>());

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
                                    ImageUrl = "http://myimageurl.com/image.jpg"
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
                            ImageUrl = "http://myimageurl.com/image.jpg"
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
                                    ImageUrl = "http://myimageurl.com/image.jpg"
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
            CategoriesController controller = new CategoriesController(mock.Object, It.IsAny<IContentLogic>(), It.IsAny<IPlaylistLogic>());

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
                                    ImageUrl = "http://myimageurl.com/image.jpg"
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
                            ImageUrl = "http://myimageurl.com/image.jpg"
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
            CategoriesController controller = new CategoriesController(mock.Object, It.IsAny<IContentLogic>(), It.IsAny<IPlaylistLogic>());

            IActionResult result = controller.Get(expectedCategoryId);

            mock.VerifyAll();
            Assert.IsTrue(result is NotFoundObjectResult);
        }

        [TestMethod]
        public void GetContentsByCategoryOk()
        {
            List<Content> expectedContents = GetContentsByCategoryExpectedContents();
            Category expectedCategory = expectedContents.First().Categories.First();

            Mock<ICategoryLogic> categoryLogic = new Mock<ICategoryLogic>(MockBehavior.Strict);
            categoryLogic.Setup(m => m.GetCategory(expectedCategory.Id)).Returns(expectedCategory);

            Mock<IContentLogic> contentLogicMock = new Mock<IContentLogic>(MockBehavior.Strict);
            contentLogicMock.Setup(m => m.GetContents(expectedCategory)).Returns(expectedContents);

            CategoriesController controller = new CategoriesController(categoryLogic.Object, contentLogicMock.Object, It.IsAny<IPlaylistLogic>());

            IActionResult result = controller.GetContents(expectedCategory.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<ContentBasicInfo> obtainedContents = objectResult.Value as IEnumerable<ContentBasicInfo>;

            contentLogicMock.VerifyAll();
            CollectionAssert.AreEqual(expectedContents.
                Select(content => new ContentBasicInfo(content)).
                ToList(),
                obtainedContents.ToList(),
                new ContentBasicInfoComparer());
        }

        private static List<Content> GetContentsByCategoryExpectedContents()
        {
            Category rock = new Category()
            {
                Id = 1,
                Name = "Rock"
            };

            Content itsMyLife = new Content()
            {
                ArtistName = "Bon Jovi",
                Categories = new List<Category>() { rock },
                PlayLists = new List<Playlist>() { },
                Id = 1,
                ContentLength = new TimeSpan(0, 2, 30),
                Name = "It's My Life",
                ImageUrl = "http://www.images.com/image.jpg"
            };

            Content livinOnAPrayer = new Content()
            {
                ArtistName = "Bon Jovi",
                Categories = new List<Category>() { rock },
                PlayLists = new List<Playlist>() { },
                Id = 2,
                ContentLength = new TimeSpan(0, 4, 10),
                Name = "Livin' On A Prayer",
                ImageUrl = "http://www.images.com/image.jpg"
            };

            rock = new Category()
            {
                Id = 1,
                Name = "Rock",
                Contents = new List<Content>() { itsMyLife, livinOnAPrayer }
            };

            List<Content> expectedContent = new List<Content>();
            foreach (Content song in rock.Contents)
            {
                expectedContent.Add(song);
            }
            return expectedContent;
        }

        [TestMethod]
        public void GetPlaylistsByCategoryOk()
        {         
            List<Playlist> expectedPlaylists = GetPlaylistsByCategoryOkExpected();
            Category expectedCategory = expectedPlaylists.First().Categories.First();
            Mock<IPlaylistLogic> playlistLogicMock = new Mock<IPlaylistLogic>(MockBehavior.Strict);
            playlistLogicMock.Setup(m => m.GetPlaylists(expectedCategory)).Returns(expectedPlaylists);
            Mock<ICategoryLogic> categoryLogicMock = new Mock<ICategoryLogic>(MockBehavior.Strict);
            categoryLogicMock.Setup(m => m.GetCategory(expectedCategory.Id)).Returns(expectedCategory);
            CategoriesController controller = new CategoriesController(categoryLogicMock.Object, It.IsAny<IContentLogic>(), playlistLogicMock.Object);

            IActionResult result = controller.GetPlaylists(expectedCategory.Id);
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<PlaylistBasicInfo> obtainedPlaylists = objectResult.Value as IEnumerable<PlaylistBasicInfo>;

            playlistLogicMock.VerifyAll();
            CollectionAssert.AreEqual(expectedPlaylists.
                Select(playlist => new PlaylistBasicInfo(playlist)).ToList(),
                obtainedPlaylists.ToList(),
                new PlaylistBasicInfoComparer());
        }

        private List<Playlist> GetPlaylistsByCategoryOkExpected()
        {
            Category rock = new Category()
            {
                Id = 1,
                Name = "Rock"
            };

            Playlist bonJoviPlaylist = new Playlist()
            {
                Id = 1,
                Name = "The Best of Bon Jovi",
                Description = "The Best song of all time by Bon Jovi",
                ImageUrl = "http://www.images.com/image.jpg",
                Categories = new List<Category>() { rock },
                Contents = new List<Content>() { }
            };

            Playlist greenDayPlaylist = new Playlist()
            {
                Id = 2,
                Name = "The Best of Green Day",
                Description = "The Best song of all time by Green Day",
                ImageUrl = "http://www.images.com/image.jpg",
                Categories = new List<Category>() { rock },
                Contents = new List<Content>() { }
            };

            rock = new Category()
            {
                Id = 1,
                Name = "Rock",
                PlayLists = new List<Playlist>() { bonJoviPlaylist, greenDayPlaylist }
            };

            List<Playlist> expectedPlaylists = new List<Playlist>();
            foreach (Playlist playlist in rock.PlayLists)
            {
                expectedPlaylists.Add(playlist);
            }
            return expectedPlaylists;
        }
    }
}
