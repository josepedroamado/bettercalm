using Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
	[TestClass]
	public class PlaylistTest
	{
		[TestMethod]
		public void PlaylistValidateOk()
		{
			Playlist playlist = new Playlist()
			{
				Description = "Description",
				Id = 1,
				Name = "name"
			};

			Assert.IsTrue(playlist.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void PlaylistWithoutDescription()
		{
			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "name"
			};

			Assert.IsFalse(playlist.Validate());
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidInputException))]
		public void PlaylistWithoutName()
		{
			Playlist playlist = new Playlist()
			{
				Description = "Description",
				Id = 1
			};

			Assert.IsFalse(playlist.Validate());
		}
	}
}
