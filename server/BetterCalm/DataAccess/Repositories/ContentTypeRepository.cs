using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
			ContentType contentType = this.contentTypes.FirstOrDefault(type => type.Name.Equals(name));
			if (contentType == null)
            {
				throw new NotFoundException(name);
			}
			return contentType;
		}
	}
}
