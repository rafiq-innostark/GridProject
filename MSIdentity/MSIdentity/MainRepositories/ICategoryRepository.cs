using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSIdentity.Models;
using MSIdentity.Repository;

namespace MSIdentity.MainRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category, int>
    {
        IEnumerable<Category> GetAllCategories();
    }
}
