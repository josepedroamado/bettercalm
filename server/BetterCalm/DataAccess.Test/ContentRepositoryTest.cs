using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace DataAccess.Test
{
	[TestClass]
	public class ContentRepositoryTest
	{
		private DbContext context;
		private DbConnection connection;

		public ContentRepositoryTest()
		{
			this.connection = new SqliteConnection("Filename=:memory:");
			this.context = new BetterCalmContext(
				new DbContextOptionsBuilder<BetterCalmContext>()
				.UseSqlite(connection)
				.Options);
		}

		[TestInitialize]
		public void Setup()
		{
			this.connection.Open();
			this.context.Database.EnsureCreated();
		}

		[TestCleanup]
		public void TestCleanup()
		{
			this.context.Database.EnsureDeleted();
		}

		[TestMethod]
		public void GetAllOk()
		{
			List<Content> expectedContents = GetExpectedContents();

			expectedContents.ForEach(content => this.context.Add(content));
			this.context.SaveChanges();
			ContentRepository repository = new ContentRepository(this.context);

			IEnumerable<Content> obtainedContents = repository.GetAll();
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
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
		public void GetAllByPlaylistOk()
		{
			List<Content> expectedContents = GetAllByPlaylistExpectedContents();

			expectedContents.ForEach(content => this.context.Add(content));
			this.context.SaveChanges();
			ContentRepository repository = new ContentRepository(this.context);

			Playlist expectedPlaylist = expectedContents.First().PlayLists.First();
			IEnumerable<Content> obtainedContents = repository.GetAll(expectedPlaylist);
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
		}

		private static List<Content> GetAllByPlaylistExpectedContents()
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
