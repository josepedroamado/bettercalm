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
			ContentTypeRepository typesRepository = new ContentTypeRepository(this.context);
			if (content.Validate() && typesRepository.Get(content.ContentType.Name) != null)
			{
				this.contents.Add(content);
				this.context.SaveChanges();
			}
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
			Content content = this.contents
				.Include("PlayLists")
				.Include("Categories")
				.FirstOrDefault(cont => cont.Id == id);
			if (content == null)
				throw new NotFoundException(id.ToString());
			return content;
		}

		public IEnumerable<Content> GetAll()
		{
			if (this.contents.Count() <= 0)
				throw new CollectionEmptyException("Contents");
			else
				return this.contents;
		}

        public IEnumerable<Content> GetAll(Playlist playlist)
        {
			if (this.contents.Count() <= 0)
				throw new CollectionEmptyException("Contents");
			else
				return this.contents.Where(content => content.PlayLists.Contains(playlist));
		}

        public IEnumerable<Content> GetAll(Category category)
        {
			if (this.contents.Count() <= 0)
				throw new CollectionEmptyException("Contents");
			else
				return this.contents.Where(content => content.Categories.Contains(category));
		}

		public void Update(Content content)
		{
			if (content.Validate())
			{
				this.contents.Update(content);
				this.context.SaveChanges();
			}
		}
	}
}
