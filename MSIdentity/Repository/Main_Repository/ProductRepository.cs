using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Interfaces.Main_Repositories;
using Microsoft.Practices.Unity;
using Models.Models;
using MSIdentity.Models;
using Repository.Repository;

namespace Repository.Main_Repository
{
    public sealed class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        #region Private
        /// <summary>
        /// Order by Column Names Dictionary statements - for Product
        /// </summary>
        private readonly Dictionary<OrderProductByColumn, Func<Product, object>> productClause =
              new Dictionary<OrderProductByColumn, Func<Product, object>>
                    {
                        { OrderProductByColumn.Name, c => c.Name },
                        { OrderProductByColumn.Description, c => c.Description },
                        { OrderProductByColumn.Price, c => c.Price }
                    };
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductRepository(IUnityContainer container)
            : base(container)
        {
        }

        #endregion

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Product> DbSet
        {
            get
            {
                return db.Products;
            }
        }
        public IQueryable<Product> GetAllProducts(SearchRequestModel request)
        {
            var products =
                (request.CategoryId != null || !String.IsNullOrEmpty(request.SearchString)) ? DbSet.Where(s =>
                    (s.CategoryId == request.CategoryId || request.CategoryId == null)
                    &&
                    (s.Name.Contains(request.SearchString) || String.IsNullOrEmpty(request.SearchString))).Include("Category") : DbSet.Select(s => s).Include("Category");
            if (request.SortBy == 4)
            {
                products = request.IsAsc ? products.OrderBy(x => x.Category.Name).AsQueryable() : products.OrderByDescending(x => x.Category.Name).AsQueryable(); 
               
            }
            else
            {
                products = request.IsAsc ? products.OrderBy(productClause[request.ProductOrderBy]).AsQueryable() : products.OrderByDescending(productClause[request.ProductOrderBy]).AsQueryable(); 
      
            }
            return products;
        }


    }
}