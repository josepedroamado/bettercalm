using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Domain.Test
{
	[TestClass]
	public class ContentTest
	{
		[TestMethod]
		public void UpdateFromContentOk()
		{
			Content empty = new Content();
			Content source = new Content()
			{
				ArtistName = "ArtistName",
				AudioUrl = "http://audio.com/audio.mp3",
				Categories = new List<Category>()
				{
					new Category()
					{
						Id = 1
					}
				},
				ContentLength = TimeSpan.Parse("00:02:00"),
				Id = 1,
				ImageUrl = "http://image.com/image.jpg",
				Name = "ContentName",
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1
					}
				}
			};

			empty.UpdateFromContent(source);
			Assert.AreEqual(empty, source);
		}

		[TestMethod]
		public void ValidatePlaylist()
		{
			Content content = new Content()
			{
				ArtistName = "artist name",
				AudioUrl = "http://www.images.com/image.jpg",
				ContentLength = TimeSpan.Parse("00:01:30"),
				Name = "content name",
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "playlist name",
						Description = "playlist description"
					}
				}
			};

			Assert.IsTrue(content.Validate());
		}
		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void ValidateWithoutArtistName()
		{
			Content content = new Content()
			{
				AudioUrl = "http://www.images.com/image.jpg",
				ContentLength = TimeSpan.Parse("00:01:30"),
				Name = "content name",
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "playlist name",
						Description = "playlist description"
					}
				}
			};

			Assert.IsTrue(content.Validate());
		}
		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void ValidateWithoutAudioUrl()
		{
			Content content = new Content()
			{
				ArtistName = "artist name",
				ContentLength = TimeSpan.Parse("00:01:30"),
				Name = "content name",
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "playlist name",
						Description = "playlist description"
					}
				}
			};

			Assert.IsTrue(content.Validate());
		}
		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void ValidateWithoutContentLength()
		{
			Content content = new Content()
			{
				ArtistName = "artist name",
				AudioUrl = "http://www.images.com/image.jpg",
				Name = "content name",
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "playlist name",
						Description = "playlist description"
					}
				}
			};

			Assert.IsTrue(content.Validate());
		}
		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void ValidateWithoutName()
		{
			Content content = new Content()
			{
				ArtistName = "artist name",
				AudioUrl = "http://www.images.com/image.jpg",
				ContentLength = TimeSpan.Parse("00:01:30"),
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "playlist name",
						Description = "playlist description"
					}
				}
			};

			Assert.IsTrue(content.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void ValidatePlaylists()
		{
			Content content = new Content()
			{
				ArtistName = "artist name",
				AudioUrl = "http://www.images.com/image.jpg",
				ContentLength = TimeSpan.Parse("00:01:30"),
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "playlist name"
					}
				}
			};

			Assert.IsTrue(content.Validate());
		}
	}
}
