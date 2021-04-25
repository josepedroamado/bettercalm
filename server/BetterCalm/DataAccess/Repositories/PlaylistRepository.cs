using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
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
			Playlist playlist = this.playlists.FirstOrDefault(playlist => playlist.Id == id);
			if (playlist == null)
				throw new NotFoundException(id.ToString());
			return playlist;
		}

		public IEnumerable<Playlist> GetAll()
		{
			return this.playlists;
		}
	}
}
