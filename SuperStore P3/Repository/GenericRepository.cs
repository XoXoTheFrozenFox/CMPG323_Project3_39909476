using Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EcoPower_Logistics.Repository
{
    /// <summary>
    /// Generic repository implementation for common CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TKey">The type of the primary key.</typeparam>
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
    {
        protected readonly SuperStoreContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(SuperStoreContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        /// <summary>
        /// Gets all entities of the specified type.
        /// </summary>
        /// <returns>An enumerable collection of entities.</returns>
        public IEnumerable<TEntity> GetAll() => _dbSet.ToList();

        /// <summary>
        /// Retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key value.</param>
        /// <returns>The entity with the specified primary key, or null if not found.</returns>
        public TEntity GetById(TKey id)
        {
            // Specify the primary key property name here
            string idPropertyName = "YourPrimaryKeyPropertyName"; // Replace with the correct property name
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var predicate = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(
                    Expression.Property(parameter, idPropertyName),
                    Expression.Constant(id, typeof(TKey))),
                parameter);

            return _dbSet.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Creates a new entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates an existing entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public void Edit(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key value of the entity to delete.</param>
        public void Delete(TKey id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Checks if an entity with the specified primary key exists in the repository.
        /// </summary>
        /// <param name="id">The primary key value to check.</param>
        /// <returns>True if the entity exists; otherwise, false.</returns>
        public bool Exists(TKey id)
        {
            return GetById(id) != null;
        }
    }
}