using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Test
{
	[TestClass]
	public class CategoryLogicTest
	{
        [TestMethod]
        public void GetAll_CategoriesExist_CategoriesFetched()
        {
            List<Category> expectedCategories = GetExpectedCategories();
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(m => m.GetAll()).Returns(expectedCategories);

            CategoryLogic categoryLogic = new CategoryLogic(categoryRepositoryMock.Object);

            IEnumerable<Category> obtainedCategories = categoryLogic.GetCategories();
            categoryRepositoryMock.VerifyAll();
            Assert.IsTrue(obtainedCategories.SequenceEqual(expectedCategories));
        }

        private List<Category> GetExpectedCategories()
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
        public void Get_CategoryExists_CategoryFetched()
        {
            Category expectedCategory = GetExpectedCategory();
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(m => m.Get(expectedCategory.Id)).Returns(expectedCategory);
            CategoryLogic categoryLogic = new CategoryLogic(categoryRepositoryMock.Object);

            Category obtainedCategory = categoryLogic.GetCategory(expectedCategory.Id);
            categoryRepositoryMock.VerifyAll();
            Assert.AreEqual(expectedCategory, obtainedCategory);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Get_CategoryNotFound_ExceptionThrown()
        {
            int expectedCategoryId = 1;
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(m => m.Get(expectedCategoryId)).Throws(new NotFoundException(expectedCategoryId.ToString()));
            CategoryLogic categoryLogic = new CategoryLogic(categoryRepositoryMock.Object);

            Category obtainedCategory = categoryLogic.GetCategory(expectedCategoryId);
            categoryRepositoryMock.VerifyAll();
            Assert.IsNull(obtainedCategory);
        }

        private Category GetExpectedCategory()
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
    }
}
