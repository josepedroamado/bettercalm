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
using WebAPI.Controllers;

namespace WebAPI.Test
{
	[TestClass]
	public class PlaylistsControllerTest
	{
		[TestMethod]
		public void GetPlaylistsOk()
		{
			List<Playlist> expectedPlaylists = GetPlaylistsOkExpected();
			
			Mock<IPlaylistLogic> mock = new Mock<IPlaylistLogic>(MockBehavior.Strict);
			mock.Setup(m => m.GetPlaylists()).Returns(expectedPlaylists);
			PlaylistsController controller = new PlaylistsController(mock.Object, It.IsAny<IContentLogic>());

			IActionResult result = controller.Get();
			OkObjectResult objectResult = result as OkObjectResult;
			IEnumerable<PlaylistBasicInfo> obtainedPlaylists = objectResult.Value as IEnumerable<PlaylistBasicInfo>;

			mock.VerifyAll();
			CollectionAssert.AreEqual(expectedPlaylists.
				Select(playlist => new PlaylistBasicInfo(playlist)).ToList(),
				obtainedPlaylists.ToList(),
				new PlaylistBasicInfoComparer());
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
		public void GetPlaylistByIdOk()
		{
			Playlist expectedPlaylist = GetPlaylistOkExpected();

			Mock<IPlaylistLogic> mock = new Mock<IPlaylistLogic>(MockBehavior.Strict);
			mock.Setup(m => m.GetPlaylist(expectedPlaylist.Id)).Returns(expectedPlaylist);
			PlaylistsController controller = new PlaylistsController(mock.Object, It.IsAny<IContentLogic>());

			IActionResult result = controller.Get(expectedPlaylist.Id);
			OkObjectResult objectResult = result as OkObjectResult;
			Playlist obtainedPlaylist = objectResult.Value as Playlist;

			mock.VerifyAll();
			Assert.AreEqual(obtainedPlaylist, expectedPlaylist);
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
		public void GetPlaylistByIdNotFound()
		{
			int expectedPlaylistId = 1;

			Mock<IPlaylistLogic> mock = new Mock<IPlaylistLogic>(MockBehavior.Strict);
			mock.Setup(m => m.GetPlaylist(expectedPlaylistId)).Throws(new NotFoundException(expectedPlaylistId.ToString()));
			PlaylistsController controller = new PlaylistsController(mock.Object, It.IsAny<IContentLogic>());

			IActionResult result = controller.Get(expectedPlaylistId);

			mock.VerifyAll();
			Assert.IsTrue(result is NotFoundObjectResult);
		}

		[TestMethod]
		public void GetContentsByPlaylistOk()
		{
			List<Content> expectedContents = GetContentsByPlaylistExpectedContents();
			Playlist expectedPlaylist = expectedContents.First().PlayLists.First();

			Mock<IPlaylistLogic> playlistLogicMock = new Mock<IPlaylistLogic>(MockBehavior.Strict);
			playlistLogicMock.Setup(m => m.GetPlaylist(expectedPlaylist.Id)).Returns(expectedPlaylist);

			Mock<IContentLogic> contentLogicMock = new Mock<IContentLogic>(MockBehavior.Strict);
			contentLogicMock.Setup(m => m.GetContents(expectedPlaylist)).Returns(expectedContents);

			PlaylistsController controller = new PlaylistsController(playlistLogicMock.Object, contentLogicMock.Object);

			IActionResult result = controller.GetContents(expectedPlaylist.Id);
			OkObjectResult objectResult = result as OkObjectResult;
			IEnumerable<ContentBasicInfo> obtainedContents = objectResult.Value as IEnumerable<ContentBasicInfo>;

			contentLogicMock.VerifyAll();
			CollectionAssert.AreEqual(expectedContents.
				Select(content => new ContentBasicInfo(content)).
				ToList(),
				obtainedContents.ToList(),
				new ContentBasicInfoComparer());
		}

		private static List<Content> GetContentsByPlaylistExpectedContents()
		{
			Category rock = new Category()
			{
				Id = 1,
				Name = "Rock"
			};

			Playlist bestOfBonJovi = new Playlist() { };

			Content itsMyLife = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>() { },
				PlayLists = new List<Playlist>() { bestOfBonJovi },
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg"
			};

			Content livinOnAPrayer = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>() { },
				PlayLists = new List<Playlist>() { bestOfBonJovi },
				Id = 2,
				ContentLength = new TimeSpan(0, 4, 10),
				Name = "Livin' On A Prayer",
				ImageUrl = "http://www.images.com/image.jpg"
			};

			bestOfBonJovi = new Playlist()
			{
				Id = 1,
				Name = "Best of Bon Jovi",
				Description = "The Best songs by Bon Jovi",
				ImageUrl = "http://www.images.com/image.jpg",
				Categories = new List<Category>() { rock },
				Contents = new List<Content>() { itsMyLife, livinOnAPrayer }
			};

			List<Content> expectedContent = new List<Content>();
			foreach (Content song in bestOfBonJovi.Contents)
			{
				expectedContent.Add(song);
			}

			return expectedContent;
		}
	}
}
