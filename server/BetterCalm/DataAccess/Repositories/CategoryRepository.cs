using DataAccessInterfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            throw new System.NotImplementedException();
        }
    }
}
