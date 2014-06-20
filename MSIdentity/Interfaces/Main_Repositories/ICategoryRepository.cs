using System.Collections.Generic;
using Interfaces.Repository;
using Models.Models;
using MSIdentity.Models;

namespace Interfaces.Main_Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category, int>
    {
        IEnumerable<Category> GetAllCategories();
    }
}
