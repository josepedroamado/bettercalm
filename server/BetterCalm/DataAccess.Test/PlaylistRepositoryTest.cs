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
	public class PlaylistRepositoryTest
	{
		private DbContext context;
		private DbConnection connection;

		public PlaylistRepositoryTest()
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
		public void GetPlaylistsOk()
		{
			List<Playlist> expectedPlaylists = GetPlaylistsOkExpected();
			expectedPlaylists.ForEach(playlist => this.context.Add(playlist));
			this.context.SaveChanges();
			PlaylistRepository repository = new PlaylistRepository(this.context);

			IEnumerable<Playlist> obtainedPlaylists = repository.GetAll();
			Assert.IsTrue(expectedPlaylists.SequenceEqual(obtainedPlaylists));
		}

		[TestMethod]
		[ExpectedException(typeof(CollectionEmptyException))]
		public void GetPlaylistsWhenNoPlaylistsExist()
		{
			PlaylistRepository repository = new PlaylistRepository(this.context);
			IEnumerable<Playlist> obtainedPlaylists = repository.GetAll();
			Assert.IsNull(obtainedPlaylists);
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
							ImageUrl = "http://myrockurl.com/rock.jpg",
							AudioUrl = "http://www.audios.com/audio.mp3"
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

		[TestMethod]
		public void GetPlaylistsByCategoryOk()
		{
			List<Playlist> expectedPlaylists = GetPlaylistsByCategoryOkExpected();
			expectedPlaylists.ForEach(playlist => this.context.Add(playlist));
			this.context.SaveChanges();
			PlaylistRepository repository = new PlaylistRepository(this.context);

			IEnumerable<Playlist> obtainedPlaylists = repository.GetAll(expectedPlaylists.First().Categories.First());
			Assert.IsTrue(expectedPlaylists.SequenceEqual(obtainedPlaylists));
		}

		[TestMethod]
		[ExpectedException(typeof(CollectionEmptyException))]
		public void GetPlaylistsByCategoryWhenNoPlaylistsExist()
		{
			List<Playlist> expectedPlaylists = GetPlaylistsByCategoryOkExpected();
			PlaylistRepository repository = new PlaylistRepository(this.context);
			IEnumerable<Playlist> obtainedPlaylists = repository.GetAll(expectedPlaylists.First().Categories.First());
			Assert.IsNull(obtainedPlaylists);
		}

		private List<Playlist> GetPlaylistsByCategoryOkExpected()
		{
			Category rock = new Category()
			{
				Id = 1,
				Name = "Rock"
			};

			Playlist bonJoviPlaylist = new Playlist()
			{
				Id = 1,
				Name = "The Best of Bon Jovi",
				Description = "The Best song of all time by Bon Jovi",
				ImageUrl = "http://www.images.com/image.jpg",
				Categories = new List<Category>() { rock },
				Contents = new List<Content>() { }
			};

			Playlist greenDayPlaylist = new Playlist()
			{
				Id = 2,
				Name = "The Best of Green Day",
				Description = "The Best song of all time by Green Day",
				ImageUrl = "http://www.images.com/image.jpg",
				Categories = new List<Category>() { rock },
				Contents = new List<Content>() { }
			};

			rock = new Category()
			{
				Id = 1,
				Name = "Rock",
				PlayLists = new List<Playlist>() { bonJoviPlaylist, greenDayPlaylist }
			};

			List<Playlist> expectedPlaylists = new List<Playlist>();
			foreach (Playlist playlist in rock.PlayLists)
			{
				expectedPlaylists.Add(playlist);
			}
			return expectedPlaylists;
		}

		[TestMethod]
		public void GetPlaylistOk()
		{
			Playlist expectedPlaylist = GetPlaylistOkExpected();
			this.context.Add(expectedPlaylist);
			this.context.SaveChanges();
			PlaylistRepository repository = new PlaylistRepository(this.context);

			Playlist obtainedPlaylist = repository.Get(expectedPlaylist.Id);
			Assert.AreEqual(expectedPlaylist, obtainedPlaylist);
		}

		[TestMethod]
		[ExpectedException(typeof(NotFoundException))]
		public void GetPlaylistNotFound()
		{
			Playlist expectedPlaylist = GetPlaylistOkExpected();
			PlaylistRepository repository = new PlaylistRepository(this.context);

			Playlist obtainedPlaylist = repository.Get(expectedPlaylist.Id);

			Assert.IsNull(obtainedPlaylist);
		}

		private Playlist GetPlaylistOkExpected()
		{
			return new Playlist
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
						ImageUrl = "http://myrockurl.com/rock.jpg",
						AudioUrl = "http://www.audios.com/audio.mp3"
					}
				}
			};
		}
	}
}
