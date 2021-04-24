using DataAccessInterfaces;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
			playlistRepositoryMock.Setup(m => m.Get()).Returns(expectedPlaylists);

            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();

            ContentPlayer contentPlayer = new ContentPlayer(playlistRepositoryMock.Object, categoryRepositoryMock.Object);

			IEnumerable<Playlist> obtainedPlaylists = contentPlayer.GetPlaylists();
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

            ContentPlayer contentPlayer = new ContentPlayer(playlistRepositoryMock.Object, categoryRepositoryMock.Object);

            IEnumerable<Category> obtainedCategories = contentPlayer.GetCategories();
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
    }
}
