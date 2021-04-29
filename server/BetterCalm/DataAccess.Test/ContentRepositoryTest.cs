using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Domain.Exceptions;
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

		[TestMethod]
		public void GetAllByCategoryOk()
		{
			List<Content> expectedContents = GetAllByCategoryExpectedContents();

			expectedContents.ForEach(content => this.context.Add(content));
			this.context.SaveChanges();
			ContentRepository repository = new ContentRepository(this.context);

			Category expectedCategory = expectedContents.First().Categories.First();
			IEnumerable<Content> obtainedContents = repository.GetAll(expectedCategory);
			Assert.IsTrue(expectedContents.SequenceEqual(obtainedContents));
		}

		private static List<Content> GetAllByCategoryExpectedContents()
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
				ImageUrl = "http://www.images.com/image.jpg",
				AudioUrl = "http://www.audios.com/audio.mp3"
			};

			this.context.Add(expectedContent);
			this.context.SaveChanges();

			ContentRepository repository = new ContentRepository(this.context);

			Content obtainedContent = repository.Get(expectedContent.Id);
			Assert.AreEqual(expectedContent, obtainedContent);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void GetContentNotFound()
		{
			Content toSaveContent = new Content()
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
				ImageUrl = "http://www.images.com/image.jpg",
				AudioUrl = "http://www.audios.com/audio.mp3"
			};

			this.context.Add(toSaveContent);
			this.context.SaveChanges();

			int toGetContentId = 2;
			
			ContentRepository repository = new ContentRepository(this.context);

			Content obtainedContent = repository.Get(toGetContentId);
			Assert.AreNotEqual(toSaveContent, obtainedContent);
		}

		[TestMethod]
		public void AddContentOkWithNewPlaylist()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					new Playlist()
					{
						Id = 1,
						Name = "Besto of Bon Jovi"
					}
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				AudioUrl = "http://www.audios.com/audio.mp3"
			};
			this.context.SaveChanges();

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(toSaveContent);
			Content added = repository.Get(toSaveContent.Id);

			Assert.AreEqual(toSaveContent, added);
		}

		[TestMethod]
		public void AddContentOkWithExistentPlaylist()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);

			Playlist playlist = new Playlist()
			{
				Id = 1,
				Name = "Besto of Bon Jovi"
			};
			this.context.Add(playlist);

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				PlayLists = new List<Playlist>()
				{
					playlist
				},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				AudioUrl = "http://www.audios.com/audio.mp3"
			};
			this.context.SaveChanges();

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(toSaveContent);
			Content added = repository.Get(toSaveContent.Id);

			Assert.AreEqual(toSaveContent, added);
		}

		[TestMethod]
		public void AddContentOkWithoutPlaylist()
		{
			Category music = new Category()
			{
				Id = 1,
				Name = "Musica"
			};
			this.context.Add(music);

			Content toSaveContent = new Content()
			{
				ArtistName = "Bon Jovi",
				Categories = new List<Category>(){
						music
					},
				Id = 1,
				ContentLength = new TimeSpan(0, 2, 30),
				Name = "It's My Life",
				ImageUrl = "http://www.images.com/image.jpg",
				AudioUrl = "http://www.audios.com/audio.mp3"
			};
			this.context.SaveChanges();

			ContentRepository repository = new ContentRepository(this.context);

			repository.Add(toSaveContent);
			Content added = repository.Get(toSaveContent.Id);

			Assert.AreEqual(toSaveContent, added);
		}
	}
}
