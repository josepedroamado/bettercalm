using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
	public class ContentRepository : IContentRepository
	{
		private DbContext context;
		private DbSet<Content> contents;

		public ContentRepository(DbContext context)
		{
			this.context = context;
			this.contents = context.Set<Content>();
		}
		public IEnumerable<Content> GetAll()
		{
			return this.contents;
		}

        public IEnumerable<Content> GetAll(Playlist playlist)
        {
			return this.contents.Where(content => content.PlayLists.Contains(playlist));
        }
    }
}
