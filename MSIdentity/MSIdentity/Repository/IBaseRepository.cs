﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSIdentity.Repository
{
    /// <summary>
    /// Base repository interface
    /// </summary>
    public interface IBaseRepository<TDomainClass, TKeyType>
        where TDomainClass : class
    {

        /// <summary>
        /// Create object instance
        /// </summary>
        /// <returns>object instance</returns>
        TDomainClass Create();

        /// <summary>
        /// Save changes
        /// </summary>
        void Update(TDomainClass instance);

        /// <summary>
        /// Delete an entry
        /// </summary>
        void Delete(TDomainClass instance);

        /// <summary>
        /// Add an entry
        /// </summary>
        /// <param name="instance"></param>
        void Add(TDomainClass instance);

        /// <summary>
        /// Find entry by key
        /// </summary>
        IQueryable<TDomainClass> Find(TDomainClass instance);

        TDomainClass Find(TKeyType id);

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        IQueryable<TDomainClass> GetAll();
        /// <summary>
        /// Save changes
        /// </summary>
        void SaveChanges();

    
    }
}
