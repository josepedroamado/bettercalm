using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BL.Test
{
	[TestClass]
	public class ImporterLogicTest
	{
		[TestMethod]
		public void Import_JSON_OK()
		{
			Playlist playlist = new Playlist()
			{
				Name = "Trending",
				Description = "Trending music"
			};
			ContentType contentType = new ContentType()
			{
				Id = 1,
				Name = "audio"
			};
			Category category = new Category()
			{
				Id = 1,
				Name = "music"
			};
			Content content = new Content()
			{
				Name = "Beat It extended",
				ContentLength = new TimeSpan(00, 06, 30),
				ArtistName = "Michael Jackson",
				ImageUrl = "http://images.com/image3.jpg",
				ContentUrl = "http://audio.com/audio3.mp3",
				Categories = new List<Category>() { category },
				ContentType = contentType,
				PlayLists = new Playlist[] { playlist }
			};

			List<Content> contents = new List<Content>() { content };

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(It.IsAny<Content>()));
			contentRepositoryMock.Setup(m => m.GetAll()).Returns(contents);

			Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepositoryMock.Setup(m => m.Get(playlist.Id)).Throws(new NotFoundException(playlist.Id.ToString()));

			Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepositoryMock.Setup(m => m.Get(category.Id)).Returns(category);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepositoryMock.Object, categoryRepositoryMock.Object);
			ImporterLogic importerLogic = new ImporterLogic(contentLogic);

			string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(basePath, "importTest.json");
			string fileType = "JSON";

			importerLogic.Import(fileType, filePath);

			List<Content> obtainedContents = contentLogic.GetContents().ToList();

			Assert.IsTrue(obtainedContents.Any(obtainedContent => {
				obtainedContent.Id = 0;
				return obtainedContent.Equals(content);
			}));
		}

		[TestMethod]
		public void Import_XML_OK()
		{
			Playlist playlist = new Playlist()
			{
				Name = "Trending",
				Description = "Trending music"
			};
			ContentType contentType = new ContentType()
			{
				Id = 1,
				Name = "audio"
			};
			Category category = new Category()
			{
				Id = 1,
				Name = "music"
			};
			Content content = new Content()
			{
				Name = "Beat It extended",
				ContentLength = new TimeSpan(00, 06, 30),
				ArtistName = "Michael Jackson",
				ImageUrl = "http://images.com/image3.jpg",
				ContentUrl = "http://audio.com/audio3.mp3",
				Categories = new List<Category>() { category },
				ContentType = contentType,
				PlayLists = new Playlist[] { playlist }
			};

			List<Content> contents = new List<Content>() { content };

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(It.IsAny<Content>()));
			contentRepositoryMock.Setup(m => m.GetAll()).Returns(contents);

			Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepositoryMock.Setup(m => m.Get(playlist.Id)).Throws(new NotFoundException(playlist.Id.ToString()));

			Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepositoryMock.Setup(m => m.Get(category.Id)).Returns(category);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepositoryMock.Object, categoryRepositoryMock.Object);
			ImporterLogic importerLogic = new ImporterLogic(contentLogic);

			string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(basePath, "importTest.xml");
			string fileType = "XML";

			importerLogic.Import(fileType, filePath);

			List<Content> obtainedContents = contentLogic.GetContents().ToList();

			Assert.IsTrue(obtainedContents.Any(obtainedContent => {
				obtainedContent.Id = 0;
				return obtainedContent.Equals(content);
			}));
		}

		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void Import_JSON_NotFound()
		{
			Playlist playlist = new Playlist()
			{
				Name = "Trending",
				Description = "Trending music"
			};
			ContentType contentType = new ContentType()
			{
				Id = 1,
				Name = "audio"
			};
			Category category = new Category()
			{
				Id = 1,
				Name = "music"
			};
			Content content = new Content()
			{
				Name = "Beat It extended",
				ContentLength = new TimeSpan(00, 06, 30),
				ArtistName = "Michael Jackson",
				ImageUrl = "http://images.com/image3.jpg",
				ContentUrl = "http://audio.com/audio3.mp3",
				Categories = new List<Category>() { category },
				ContentType = contentType,
				PlayLists = new Playlist[] { playlist }
			};

			List<Content> contents = new List<Content>() { content };

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(It.IsAny<Content>()));
			contentRepositoryMock.Setup(m => m.GetAll()).Returns(contents);

			Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepositoryMock.Setup(m => m.Get(playlist.Id)).Throws(new NotFoundException(playlist.Id.ToString()));

			Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepositoryMock.Setup(m => m.Get(category.Id)).Returns(category);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepositoryMock.Object, categoryRepositoryMock.Object);
			ImporterLogic importerLogic = new ImporterLogic(contentLogic);

			string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(basePath, "notfound.json");
			string fileType = "JSON";

			importerLogic.Import(fileType, filePath);

			List<Content> obtainedContents = contentLogic.GetContents().ToList();

			Assert.IsTrue(obtainedContents.Any(obtainedContent => {
				obtainedContent.Id = 0;
				return obtainedContent.Equals(content);
			}));
		}

		[TestMethod]
		[ExpectedException(typeof(FileNotFoundException))]
		public void Import_XML_NotFound()
		{
			Playlist playlist = new Playlist()
			{
				Name = "Trending",
				Description = "Trending music"
			};
			ContentType contentType = new ContentType()
			{
				Id = 1,
				Name = "audio"
			};
			Category category = new Category()
			{
				Id = 1,
				Name = "music"
			};
			Content content = new Content()
			{
				Name = "Beat It extended",
				ContentLength = new TimeSpan(00, 06, 30),
				ArtistName = "Michael Jackson",
				ImageUrl = "http://images.com/image3.jpg",
				ContentUrl = "http://audio.com/audio3.mp3",
				Categories = new List<Category>() { category },
				ContentType = contentType,
				PlayLists = new Playlist[] { playlist }
			};

			List<Content> contents = new List<Content>() { content };

			Mock<IContentRepository> contentRepositoryMock = new Mock<IContentRepository>(MockBehavior.Strict);
			contentRepositoryMock.Setup(m => m.Add(It.IsAny<Content>()));
			contentRepositoryMock.Setup(m => m.GetAll()).Returns(contents);

			Mock<IPlaylistRepository> playlistRepositoryMock = new Mock<IPlaylistRepository>(MockBehavior.Strict);
			playlistRepositoryMock.Setup(m => m.Get(playlist.Id)).Throws(new NotFoundException(playlist.Id.ToString()));

			Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>(MockBehavior.Strict);
			categoryRepositoryMock.Setup(m => m.Get(category.Id)).Returns(category);

			ContentLogic contentLogic = new ContentLogic(contentRepositoryMock.Object, playlistRepositoryMock.Object, categoryRepositoryMock.Object);
			ImporterLogic importerLogic = new ImporterLogic(contentLogic);

			string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
			string filePath = Path.Combine(basePath, "notfound.xml");
			string fileType = "XML";

			importerLogic.Import(fileType, filePath);

			List<Content> obtainedContents = contentLogic.GetContents().ToList();

			Assert.IsTrue(obtainedContents.Any(obtainedContent => {
				obtainedContent.Id = 0;
				return obtainedContent.Equals(content);
			}));
		}
	}
}
