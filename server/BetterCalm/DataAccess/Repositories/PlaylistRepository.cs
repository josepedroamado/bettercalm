using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
	public class PlaylistRepository : IPlaylistRepository
	{
		private readonly DbContext context;
		private readonly DbSet<Playlist> playlists;

		public PlaylistRepository(DbContext context)
		{
			this.context = context;
			this.playlists = context.Set<Playlist>();
		}

		public Playlist Get(int id)
		{
			return this.playlists.First(playlist => playlist.Id == id);
		}

		public IEnumerable<Playlist> GetAll()
		{
			return this.playlists;
		}
	}
}
