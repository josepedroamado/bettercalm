using DataAccessInterfaces;
using Domain;
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
            return this.categories;
        }

        public Category Get(int Id)
        {
            return this.categories.First(category => category.Id == Id);
        }
    }
}
