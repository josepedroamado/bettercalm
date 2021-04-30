using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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
			Assert.IsTrue(empty.Id == source.Id &&
				empty.ArtistName.Equals(source.ArtistName) &&
				empty.AudioUrl.Equals(source.AudioUrl) &&
				empty.Categories.SequenceEqual(source.Categories) &&
				empty.ContentLength.Equals(source.ContentLength) &&
				empty.ImageUrl.Equals(source.ImageUrl) &&
				empty.Name.Equals(source.Name) &&
				empty.PlayLists.SequenceEqual(source.PlayLists));
		}
	}
}
