using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MSIdentity.Models;
using MSIdentity.Repository;

namespace MSIdentity.MainRepositories
{
    public interface IProductRepository : IBaseRepository<Product, int>
    {
        IQueryable<Product> GetAllProducts(SearchRequestModel requestModel);
    }
}
