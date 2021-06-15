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
		private DbContext context;
		private DbSet<Playlist> playlists;

		public PlaylistRepository(DbContext context)
		{
			this.context = context;
			this.playlists = context.Set<Playlist>();
		}

		public Playlist Get(int id)
		{
			Playlist playlist = this.playlists.Include(x => x.Categories).FirstOrDefault(playlist => playlist.Id == id);
			if (playlist == null)
            {
				throw new NotFoundException(id.ToString());
			}
			return playlist;
		}

		public IEnumerable<Playlist> GetAll()
		{
			if (this.playlists.Count() <= 0)
            {
				throw new CollectionEmptyException("Playlists");
			}
            else
            {
				return this.playlists.Include("Categories");
			}
		}

        public IEnumerable<Playlist> GetAll(Category category)
        {
			if (this.playlists.Count() <= 0)
			{
				throw new CollectionEmptyException("Playlists");
			}
            else
			{
				return this.playlists.Where(playlist => playlist.Categories.Contains(category));
			}
		}
    }
}
