using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface ICategoryLogic
	{
		public IEnumerable<Category> GetCategories();

		public Category GetCategory(int Id);
	}
}
