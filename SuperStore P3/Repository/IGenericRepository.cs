using System;
using System.Collections.Generic;

namespace EcoPower_Logistics.Repository
{
    /// <summary>
    /// Generic repository interface for common CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TKey">The type of the primary key.</typeparam>
    public interface IGenericRepository<TEntity, TKey> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(TKey id);
        void Create(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TKey id);
        bool Exists(TKey id);
    }
}