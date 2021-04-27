using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface IContentRepository
	{
		IEnumerable<Content> GetAll();
	}
}
