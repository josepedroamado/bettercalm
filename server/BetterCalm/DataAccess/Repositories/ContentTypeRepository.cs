using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Repositories
{
	public class ContentTypeRepository : IContentTypeRepository
	{
		private DbContext context;
		private DbSet<ContentType> contentTypes;

		public ContentTypeRepository(DbContext context)
		{
			this.context = context;
			this.contentTypes = context.Set<ContentType>();
		}

		public ContentType Get(string name)
		{
			throw new NotImplementedException();
		}
	}
}
