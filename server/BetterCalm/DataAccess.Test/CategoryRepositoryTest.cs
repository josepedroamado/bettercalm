using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Domain.Exceptions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccess.Test
{
	[TestClass]
    public class CategoryRepositoryTest
    {
        private DbContext context;
        private DbConnection connection;

		public CategoryRepositoryTest()
		{
            this.connection = new SqliteConnection("Filename=:memory:");
            this.context = new BetterCalmContext(
                new DbContextOptionsBuilder<BetterCalmContext>()
                .UseSqlite(connection)
                .Options);
        }

        [TestInitialize]
        public void Setup()
        {
            this.connection.Open();
            this.context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAll_CategoriesExist_Fetched()
        {
            List<Category> expectedCategories = GetAllExpectedCategories();
            
            foreach(Category category in expectedCategories)
            {
                this.context.Add(category);
            }
            this.context.SaveChanges();
            CategoryRepository categoryRepository = new CategoryRepository(this.context);

            IEnumerable<Category> obtainedCategories = categoryRepository.GetAll();
            Assert.IsTrue(expectedCategories.SequenceEqual(obtainedCategories));
        }

        private List<Category> GetAllExpectedCategories()
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
        [ExpectedException(typeof(CollectionEmptyException))]
        public void GetAll_NoCategories_ExceptionThrown()
        {
            CategoryRepository categoryRepository = new CategoryRepository(this.context);
            IEnumerable<Category> obtainedCategories = categoryRepository.GetAll();
            Assert.IsNull(obtainedCategories);
        }

        [TestMethod]
        public void Get_CategoryExists_Fetched()
        {
            Category expectedCategory = GetExpectedCategory();
            this.context.Add(expectedCategory);
            this.context.SaveChanges();
            CategoryRepository categoryRepository = new CategoryRepository(this.context);
            
            Category obtainedCategory = categoryRepository.Get(expectedCategory.Id);
            
            Assert.AreEqual(expectedCategory, obtainedCategory);
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

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Get_CategoryNotFound_ExceptionThrown()
        {
            Category expectedCategory = GetExpectedCategory();
            CategoryRepository categoryRepository = new CategoryRepository(this.context);

            Category obtainedCategory = categoryRepository.Get(expectedCategory.Id);

            Assert.IsNull(obtainedCategory);
        }
    }
}
