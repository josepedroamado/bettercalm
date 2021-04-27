using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess.Repositories
{
	public class ContentRepository : IContentRepository
	{

		public ContentRepository(DbContext context)
		{

		}
		public IEnumerable<Content> GetAll()
		{
			throw new NotImplementedException();
		}
	}
}
