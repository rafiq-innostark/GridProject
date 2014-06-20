using System.Linq;
using Interfaces.Repository;
using Models.Models;
using MSIdentity.Models;

namespace Interfaces.Main_Repositories
{
    public interface IProductRepository : IBaseRepository<Product, int>
    {
        IQueryable<Product> GetAllProducts(SearchRequestModel requestModel);
    }
}
