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
		public void UpdateFromContent_DataIsValid_Updated()
		{
			Content empty = new Content();
			Content source = new Content()
			{
				ArtistName = "ArtistName",
				ContentUrl = "http://audio.com/audio.mp3",
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
		public void ValidatePlaylist_DataIsCorrect_Validated()
		{
			Content content = new Content()
			{
				ArtistName = "artist name",
				ContentUrl = "http://www.images.com/image.jpg",
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
		public void Validate_NoArtistName_ExceptionThrown()
		{
			Content content = new Content()
			{
				ContentUrl = "http://www.images.com/image.jpg",
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

			Assert.IsFalse(content.Validate());
		}
		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Validate_NoAudioUrl_ExceptionThrown()
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

			Assert.IsFalse(content.Validate());
		}
		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Validate_NoContentLength_ExceptionThrown()
		{
			Content content = new Content()
			{
				ArtistName = "artist name",
				ContentUrl = "http://www.images.com/image.jpg",
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

			Assert.IsFalse(content.Validate());
		}
		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Validate_NoName_ExceptionThrown()
		{
			Content content = new Content()
			{
				ArtistName = "artist name",
				ContentUrl = "http://www.images.com/image.jpg",
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

			Assert.IsFalse(content.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void Validate_InvalidPlaylists_ExceptionThrown()
		{
			Content content = new Content()
			{
				ArtistName = "artist name",
				ContentUrl = "http://www.images.com/image.jpg",
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

			Assert.IsFalse(content.Validate());
		}
	}
}
