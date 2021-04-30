using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
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

		public void Add(Content content)
		{
			this.contents.Add(content);
			this.context.SaveChanges();
		}

		public void Delete(int id)
		{
			try
			{
				Content content = Get(id);
				if (content != null)
				{
					this.context.Remove(content);
					this.context.SaveChanges();
				}
			}
			catch(NotFoundException){}
		}

		public Content Get(int id)
		{
			Content content = this.contents.FirstOrDefault(cont => cont.Id == id);
			if (content == null)
				throw new NotFoundException(id.ToString());
			return content;
		}

		public IEnumerable<Content> GetAll()
		{
			return this.contents;
		}

        public IEnumerable<Content> GetAll(Playlist playlist)
        {
			return this.contents.Where(content => content.PlayLists.Contains(playlist));
        }

        public IEnumerable<Content> GetAll(Category category)
        {
			return this.contents.Where(content => content.Categories.Contains(category));
		}

		public void Update(Content content)
		{
			throw new System.NotImplementedException();
		}
	}
}
