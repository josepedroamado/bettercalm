using BLInterfaces;
using DataAccessInterfaces;
using Domain;
using System.Collections.Generic;

namespace BL
{
	public class CategoryLogic : ICategoryLogic
	{
		private ICategoryRepository categoryRepository;

		public CategoryLogic(ICategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}
		public IEnumerable<Category> GetCategories()
		{
			return this.categoryRepository.GetAll();
		}

		public Category GetCategory(int Id)
		{
			return this.categoryRepository.Get(Id);
		}
	}
}
