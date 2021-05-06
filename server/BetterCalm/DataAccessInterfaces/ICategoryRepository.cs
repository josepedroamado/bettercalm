using Domain;
using System.Collections.Generic;

namespace DataAccessInterfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();

        Category Get(int Id);
    }
}
