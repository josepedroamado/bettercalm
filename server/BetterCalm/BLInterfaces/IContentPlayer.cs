using Domain;
using System.Collections.Generic;

namespace BLInterfaces
{
	public interface IContentPlayer
	{
		public IEnumerable<Category> GetCategories();

		public Category GetCategory(int Id);

		public IEnumerable<Content> GetContents();
	}
}
