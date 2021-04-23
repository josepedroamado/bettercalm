using DataAccess.Context;
using DataAccess.Repositories;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Test
{
	[TestClass]
	public class PlaylistRepositoryTest
	{
		private DbContext context;
		private DbContextOptions options;

		[TestInitialize]
		public void Setup()
		{
			this.options = new DbContextOptionsBuilder<BetterCalmContext>().UseInMemoryDatabase(databaseName: "BetterCalmDB").Options;
			this.context = new BetterCalmContext(this.options);
		}

		[TestMethod]
		public void GetPlaylistsOk()
		{
			List<Playlist> expectedPlaylists = GetPlaylistsOkExpected();
			expectedPlaylists.ForEach(playlist => this.context.Add(playlist));
			this.context.SaveChanges();
			PlaylistRepository repository = new PlaylistRepository(this.context);

			IEnumerable<Playlist> obtainedPlaylists = repository.Get();
			Assert.IsTrue(expectedPlaylists.SequenceEqual(obtainedPlaylists));
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
							ImageUrl = "http://myrockurl.com/rock.jpg"
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
	}
}
