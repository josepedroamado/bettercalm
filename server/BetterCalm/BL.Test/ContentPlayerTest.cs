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
	public class ContentPlayerTest
	{
		[TestMethod]
		public void GetPlaylistsOk()
		{
			List<Playlist> expectedPlaylists = GetPlaylistsOkExpected();
			
			Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepositoryMock.Setup(m => m.GetAll()).Returns(expectedPlaylists);

            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
            Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>();

            ContentPlayer contentPlayer = new ContentPlayer(playlistRepositoryMock.Object, categoryRepositoryMock.Object, contentRepositoryMock.Object);

			IEnumerable<Playlist> obtainedPlaylists = contentPlayer.GetPlaylists();
            playlistRepositoryMock.VerifyAll();
            Assert.IsTrue(obtainedPlaylists.SequenceEqual(expectedPlaylists));
		}

		private List<Playlist> GetPlaylistsOkExpected()
		{
			return new List<Playlist>()
			{
				new Playlist()
				{
					Id = 1,
					Name = "Epic Rock",
					Description = "Best of the Rock!",
					ImageUrl = "http://myimageurl.com/image.jpg",
					Contents = new List<Content>()
					{
						new Content()
						{
							Id = 1,
							ArtistName = "Jhon Doe",
							Name = "Rocking",
							ImageUrl = "http://myrockurl.com/rock.jpg"
						}
					}
				},
				new Playlist()
				{
					Id = 2,
					Name = "Hip Hop Rewind",
					Description = "Hip Hop of 90's!",
					ImageUrl = "http://myimageurl.com/image.jpg",
					Categories = new List<Category>()
					{
						new Category()
						{
							Id = 2,
							Name = "Hip Hop"
						}
					}
				}
			};
		}

		[TestMethod]
		public void GetCategoriesOk()
        {
            List<Category> expectedCategories = GetCategoriesOkExpected();
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(m => m.GetAll()).Returns(expectedCategories);
            Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
            Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>();

            ContentPlayer contentPlayer = new ContentPlayer(playlistRepositoryMock.Object, categoryRepositoryMock.Object, contentRepositoryMock.Object);

            IEnumerable<Category> obtainedCategories = contentPlayer.GetCategories();
            categoryRepositoryMock.VerifyAll();
            Assert.IsTrue(obtainedCategories.SequenceEqual(expectedCategories));
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
        public void GetCategoryOk()
        {
            Category expectedCategory = GetCategoryOkExpected();
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(m => m.Get(expectedCategory.Id)).Returns(expectedCategory);
            Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
            Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>();
            ContentPlayer contentPlayer = new ContentPlayer(playlistRepositoryMock.Object, categoryRepositoryMock.Object, contentRepositoryMock.Object);

            Category obtainedCategory = contentPlayer.GetCategory(expectedCategory.Id);
            categoryRepositoryMock.VerifyAll();
            Assert.AreEqual(expectedCategory, obtainedCategory);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetCategoryNotFound()
        {
            int expectedCategoryId = 1;
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            categoryRepositoryMock.Setup(m => m.Get(expectedCategoryId)).Throws(new NotFoundException(expectedCategoryId.ToString()));
            Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
            Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>();
            ContentPlayer contentPlayer = new ContentPlayer(playlistRepositoryMock.Object, categoryRepositoryMock.Object, contentRepositoryMock.Object);

            Category obtainedCategory = contentPlayer.GetCategory(expectedCategoryId);
            categoryRepositoryMock.VerifyAll();
            Assert.IsNull(obtainedCategory);
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
        public void GetPlaylistOk()
        {
            Playlist expectedPlaylist = GetPlaylistOkExpected();
            Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
            playlistRepositoryMock.Setup(m => m.Get(expectedPlaylist.Id)).Returns(expectedPlaylist);
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>();
            ContentPlayer contentPlayer = new ContentPlayer(playlistRepositoryMock.Object, categoryRepositoryMock.Object, contentRepositoryMock.Object);

            Playlist obtainedPlaylist = contentPlayer.GetPlaylist(expectedPlaylist.Id);

            playlistRepositoryMock.VerifyAll();
            Assert.AreEqual(expectedPlaylist, obtainedPlaylist);
        }

        private Playlist GetPlaylistOkExpected()
        {
            return new Playlist
            {
                Id = 1,
                Name = "Epic Rock",
                Description = "Best of the Rock!",
                ImageUrl = "http://myimageurl.com/image.jpg",
                Contents = new List<Content>()
                {
                    new Content()
                    {
                        Id = 1,
                        ArtistName = "Jhon Doe",
                        Name = "Rocking",
                        ImageUrl = "http://myrockurl.com/rock.jpg"
                    }
                }
            };
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void GetPlaylistNotFound()
        {
            int expectedPlaylistId = 1;
            Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
            playlistRepositoryMock.Setup(m => m.Get(expectedPlaylistId)).Throws(new NotFoundException(expectedPlaylistId.ToString()));
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
            Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>();
            ContentPlayer contentPlayer = new ContentPlayer(playlistRepositoryMock.Object, categoryRepositoryMock.Object, contentRepositoryMock.Object);

            Playlist obtainedPlaylist = contentPlayer.GetPlaylist(expectedPlaylistId);

            playlistRepositoryMock.VerifyAll();
            Assert.IsNull(obtainedPlaylist);
        }

        [TestMethod]
        public void GetContentOk()
		{
            List<Content> expectedContents = GetExpectedContents();
            Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
            contentRepositoryMock.Setup(m => m.GetAll()).Returns(expectedContents);
            Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);

            ContentPlayer contentPlayer = new ContentPlayer(playlistRepositoryMock.Object, categoryRepositoryMock.Object, contentRepositoryMock.Object);

            IEnumerable<Content> obtainedContents = contentPlayer.GetContents();

            contentRepositoryMock.VerifyAll();
            Assert.IsTrue(obtainedContents.SequenceEqual(expectedContents));
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
