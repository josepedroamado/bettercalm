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
	public class PlaylistLogicTest
	{
		[TestMethod]
		public void GetPlaylistsOk()
		{
			List<Playlist> expectedPlaylists = GetPlaylistsOkExpected();

			Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepositoryMock.Setup(m => m.GetAll()).Returns(expectedPlaylists);

			PlaylistLogic playlistLogic = new PlaylistLogic(playlistRepositoryMock.Object);

			IEnumerable<Playlist> obtainedPlaylists = playlistLogic.GetPlaylists();
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
		public void GetPlaylistOk()
		{
			Playlist expectedPlaylist = GetPlaylistOkExpected();
			Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepositoryMock.Setup(m => m.Get(expectedPlaylist.Id)).Returns(expectedPlaylist);
			PlaylistLogic playlistLogic = new PlaylistLogic(playlistRepositoryMock.Object);

			Playlist obtainedPlaylist = playlistLogic.GetPlaylist(expectedPlaylist.Id);

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
			PlaylistLogic playlistLogic = new PlaylistLogic(playlistRepositoryMock.Object);

			Playlist obtainedPlaylist = playlistLogic.GetPlaylist(expectedPlaylistId);

			playlistRepositoryMock.VerifyAll();
			Assert.IsNull(obtainedPlaylist);
		}
	}
}
