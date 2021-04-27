using BLInterfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPI.Controllers;
using System.Linq;

namespace WebAPI.Test
{
	[TestClass]
	public class ContentsControllerTest
	{
		[TestMethod]
		public void GetContentsOk()
		{
            List<Content> expectedContents = GetExpectedContents();

            Mock<IContentLogic> mock = new Mock<IContentLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetContents()).Returns(expectedContents);
            ContentsController controller = new ContentsController(mock.Object, It.IsAny<IPlaylistLogic>());

            IActionResult result = controller.Get();
            OkObjectResult objectResult = result as OkObjectResult;
            IEnumerable<ContentBasicInfo> obtainedContents = objectResult.Value as IEnumerable<ContentBasicInfo>;

            mock.VerifyAll();
            CollectionAssert.AreEqual(expectedContents.
                Select(content => new ContentBasicInfo(content)).
                ToList(),
                obtainedContents.ToList(),
                new ContentBasicInfoComparer());

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

		[TestMethod]
		public void GetContentsByPlaylistOk()
		{
			List<Content> expectedContents = GetContentsByPlaylistExpectedContents();
			Playlist expectedPlaylist = expectedContents.First().PlayLists.First();

			Mock<IPlaylistLogic> playlistLogicMock = new Mock<IPlaylistLogic>(MockBehavior.Strict);
			playlistLogicMock.Setup(m => m.GetPlaylist(expectedPlaylist.Id)).Returns(expectedPlaylist);

			Mock<IContentLogic> contentLogicMock = new Mock<IContentLogic>(MockBehavior.Strict);
			contentLogicMock.Setup(m => m.GetContents()).Returns(expectedContents);

			ContentsController controller = new ContentsController(contentLogicMock.Object, playlistLogicMock.Object);

			IActionResult result = controller.Get(expectedPlaylist.Id);
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
