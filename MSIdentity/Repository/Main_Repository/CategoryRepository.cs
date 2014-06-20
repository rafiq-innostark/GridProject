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
    public sealed class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        

        #region Constructor
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