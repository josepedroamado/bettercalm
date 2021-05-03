using DataAccessInterfaces;
using Domain;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private DbContext context;
        private DbSet<Category> categories;

        public CategoryRepository(DbContext context)
        {
            this.context = context;
            this.categories = context.Set<Category>();
        }

        public IEnumerable<Category> GetAll()
        {
            if (this.categories.Count() <= 0)
                throw new CollectionEmptyException("Categories");
            else
                return this.categories;
        }

        public Category Get(int id)
        {
            Category category =  this.categories.FirstOrDefault(category => category.Id == id);
            if (category == null)
                throw new NotFoundException(id.ToString());
            return category;
        }
    }
}
