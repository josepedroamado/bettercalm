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
	public class ContentLogicTest
	{
        [TestMethod]
        public void GetContentsOk()
		{
            List<Content> expectedContents = GetExpectedContents();
            Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
            contentRepositoryMock.Setup(m => m.GetAll()).Returns(expectedContents);

            ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object);

            IEnumerable<Content> obtainedContents = contentLogic.GetContents();

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

		[TestMethod]
		public void GetContentsByPlaylistOk()
		{
			List<Content> expectedContents = GetContentsByPlaylistExpectedContents();
			Playlist expectedPlaylist = expectedContents.First().PlayLists.First();
			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.GetAll(expectedPlaylist)).Returns(expectedContents);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object);

			IEnumerable<Content> obtainedContents = contentLogic.GetContents(expectedPlaylist);
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
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

		[TestMethod]
		public void GetContentsByCategoryOk()
		{
			List<Content> expectedContents = GetContentsByCategoryExpectedContents();
			Category expectedCategory = expectedContents.First().Categories.First();
			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.GetAll(expectedCategory)).Returns(expectedContents);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object);

			IEnumerable<Content> obtainedContents = contentLogic.GetContents(expectedCategory);
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
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
		public void GetContentOk()
		{
			Content expectedContent = new Content()
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
			};

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Get(expectedContent.Id)).Returns(expectedContent);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object);

			Content obtainedContent = contentLogic.GetContent(expectedContent.Id);

			contentRepositoryMock.VerifyAll();
			Assert.AreEqual(expectedContent, obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void GetContentNotFound()
		{
			int testContentId = 1;

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Get(testContentId)).Throws(new NotFoundException(testContentId.ToString()));

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object);

			Content obtainedContent = contentLogic.GetContent(testContentId);

			contentRepositoryMock.VerifyAll();
			Assert.IsNull(obtainedContent);
		}
	}
}
