using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
	public interface ICategoryRepository
	{
		Category Get(int id);
		IEnumerable<Category> GetAll();
	}
}
