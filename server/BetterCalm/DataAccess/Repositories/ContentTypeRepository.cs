using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Repositories
{
	public class ContentTypeRepository : IContentTypeRepository
	{
		private DbContext context;
		private DbSet<Content> contents;

		public ContentTypeRepository(DbContext context)
		{
			this.context = context;
			this.contents = context.Set<Content>();
		}

		public ContentType Get(string name)
		{
			throw new NotImplementedException();
		}
	}
}
