using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

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

		public IEnumerable<Playlist> GetAll()
		{
			return this.playlists;
		}
	}
}
