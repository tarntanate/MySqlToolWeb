using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// determines whether any element of a sequence exists or satisfies a condition.
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>number of rows</returns>
        bool Any(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// get number of rows in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>number of rows</returns>
        int Count(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// get first item in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>entity of <typeparamref name="TEntity"/></returns>
        TEntity First(params object[] keyValues);

        /// <summary>
        /// get first item in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="include">include parameters</param>
        /// <param name="disableTracking">change tracking in entity</param>
        /// <returns>entity of <typeparamref name="TEntity"/></returns>
        TEntity First(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        /// <summary>
        /// fetch all rows in table with paging
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="include">include parameters</param>
        /// <param name="start">The index of the rows.</param>
        /// <param name="length">The size of the page.</param>
        /// <param name="disableTracking">change tracking in entity</param>
        /// <returns>entity of <typeparamref name="IEnumerable{TResult}"/></returns>
        IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int start = 0,
            int length = 20,
            bool disableTracking = true);

        /// <summary>
        /// fetch all rows in table with paging
        /// </summary>
        /// <param name="selector">expression selector</param>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="include">include parameters</param>
        /// <param name="start">The index of the rows.</param>
        /// <param name="length">The size of the page.</param>
        /// <param name="disableTracking">change tracking in entity</param>
        /// <returns>entity of <typeparamref name="IEnumerable{TResult}"/></returns>
        IEnumerable<TResult> Find<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int start = 0,
            int length = 20,
            bool disableTracking = true) where TResult : class;

        /// <summary>
        /// insert entity
        /// </summary>
        /// <param name="entity">entity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="updates">updated field(s)</param>
        void Update(object id, TEntity entity);

        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="updates">updated field(s)</param>
        void Update(object id, TEntity entity, params Expression<Func<TEntity, object>>[] properties);

        /// <summary>
        /// delete by id
        /// </summary>
        /// <param name="id">id</param>
        void Delete(params object[] keyValues);

        /// <summary>
        /// save an entity data
        /// </summary>
        int SaveChanges();

        /// <summary>
        /// determines whether any element of a sequence exists or satisfies a condition.
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>number of rows</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// get number of rows in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>number of rows</returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);

        /// <summary>
        /// get first item in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>entity of <typeparamref name="TEntity"/></returns>
        Task<TEntity> FirstAsync(params object[] keyValues);

        /// <summary>
        /// get first item in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="include">include parameters</param>
        /// <param name="disableTracking">change tracking in entity</param>
        /// <returns>entity of <typeparamref name="TEntity"/></returns>
        Task<TEntity> FirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        Task<TResult> FirstAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true);

        /// <summary>
        /// fetch all rows in table with paging
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="include">include parameters</param>
        /// <param name="start">The index of the rows.</param>
        /// <param name="length">The size of the page.</param>
        /// <param name="disableTracking">change tracking in entity</param>
        /// <returns>entity of <typeparamref name="IEnumerable{TResult}"/></returns>
        Task<IEnumerable<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int start = 0,
            int length = 20,
            bool disableTracking = true);

        /// <summary>
        /// fetch all rows in table with paging
        /// </summary>
        /// <param name="selector">expression selector</param>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="include">include parameters</param>
        /// <param name="start">The index of the rows.</param>
        /// <param name="length">The size of the page.</param>
        /// <param name="disableTracking">change tracking in entity</param>
        /// <returns>entity of <typeparamref name="IEnumerable{TResult}"/></returns>
        Task<IEnumerable<TResult>> FindAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int start = 0,
            int length = 20,
            bool disableTracking = true) where TResult : class;

        /// <summary>
        /// insert entity
        /// </summary>
        /// <param name="entity">entity</param>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="updates">updated field(s)</param>
        Task UpdateAsync(object id, TEntity entity);

        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="updates">updated field(s)</param>
        Task UpdateAsync(object id, TEntity entity, params Expression<Func<TEntity, object>>[] properties);

        /// <summary>
        /// delete by id
        /// </summary>
        /// <param name="id">id</param>
        Task DeleteAsync(params object[] keyValues);

        /// <summary>
        /// save an entity data
        /// </summary>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}