using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System.Collections.Generic;
using WebAPI.Controllers;
using System.Linq;

namespace WebAPI.Test
{
	[TestClass]
	public class PlaylistControllerTest
	{
		[TestMethod]
		public void TestMethod1()
		{
			List<Playlist> expectedPlaylists = GetPlaylistsOkExpected();
			
			Mock<IContentPlayer> mock = new Mock<IContentPlayer>(MockBehavior.Strict);
			mock.Setup(m => m.GetPlaylists()).Returns(expectedPlaylists);
			PlaylistsController controller = new PlaylistsController(mock.Object);

			IActionResult result = controller.Get();
			OkObjectResult objectResult = result as OkObjectResult;
			IEnumerable<PlaylistBasicInfo> obtainedPlaylists = objectResult.Value as IEnumerable<PlaylistBasicInfo>;

			mock.VerifyAll();
			Assert.IsTrue(expectedPlaylists.
				Select(playlist => new PlaylistBasicInfo(playlist)).
				SequenceEqual(obtainedPlaylists));

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
	}
}
