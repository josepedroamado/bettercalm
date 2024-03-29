﻿using DataAccessInterfaces;
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
		private IContentTypeRepository contentTypeRepository;

		public ContentRepository(DbContext context, IContentTypeRepository contentTypeRepository)
		{
			this.context = context;
			this.contents = context.Set<Content>();
			this.contentTypeRepository = contentTypeRepository;
		}

		public void Add(Content content)
		{
			content.ContentType = contentTypeRepository.Get(content.ContentType.Name);
			if (content.Validate())
			{
				ContentTypeRepository typesRepository = new ContentTypeRepository(this.context);
				content.ContentType = typesRepository.Get(content.ContentType.Name);
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
				.Include(c => c.PlayLists)
				.Include(c => c.Categories)
				.Include(c => c.ContentType)
				.FirstOrDefault(cont => cont.Id == id);
			if (content == null)
			{
                throw new NotFoundException(id.ToString());
            }
			return content;
		}

		public IEnumerable<Content> GetAll()
		{
			if (this.contents.Count() <= 0)
            {
				throw new CollectionEmptyException("Contents");
			}
            else
            {
				return this.contents.Include(c => c.ContentType).Include(c => c.Categories);
			}
				
		}

        public IEnumerable<Content> GetAll(Playlist playlist)
        {
			if (this.contents.Count() <= 0)
			{
				throw new CollectionEmptyException("Contents");
			}
			else
				return this.contents.Include(c => c.ContentType).Include(c => c.Categories)
					.Where(content => content.PlayLists.Contains(playlist));
		}

        public IEnumerable<Content> GetAll(Category category)
        {
			if (this.contents.Count() <= 0)
            {
				throw new CollectionEmptyException("Contents");
			}
            else
            {
				return this.contents.Include(c => c.ContentType).Where(content => content.Categories.Contains(category));
			}
		}

		public IEnumerable<Content> GetAll(string contentType)
		{
			return this.contents.Include(c => c.ContentType).Where(content => content.ContentType.Name.Equals(contentType));
		}

		public void Update(Content content)
		{
			content.ContentType = contentTypeRepository.Get(content.ContentType.Name);
			if (content.Validate())
			{
				this.contents.Update(content);
				this.context.SaveChanges();
			}
		}
	}
}
