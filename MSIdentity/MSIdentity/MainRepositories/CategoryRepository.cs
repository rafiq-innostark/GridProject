using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MSIdentity.Models;
using MSIdentity.Repository;

namespace MSIdentity.MainRepositories
{
    public sealed class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        

        #region Constructor
        ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Constructor
        /// </summary>
        public CategoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        #endregion
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Category> DbSet
        {
            get
            {
                return db.Categories;
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return DbSet.ToList();
        }
    }
}