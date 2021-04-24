using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Test
{
    [TestClass]
    public class CategoryRepositoryTest
    {
        private DbContext context;
        private DbContextOptions options;

        [TestInitialize]
        public void Setup()
        {
            this.options = new DbContextOptionsBuilder<BetterCalmContext>().UseInMemoryDatabase(databaseName: "BetterCalmDB_CategoryRepository").Options;
            this.context = new BetterCalmContext(this.options);
        }

        [TestMethod]
        public void GetCategoriesOk()
        {
            List<Category> expectedCategories = GetCategoriesOkExpected();
            
            foreach(Category category in expectedCategories)
            {
                this.context.Add(category);
            }
            this.context.SaveChanges();
            CategoryRepository categoryRepository = new CategoryRepository(this.context);

            IEnumerable<Category> obtainedCategories = categoryRepository.GetAll();
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
    }
}
