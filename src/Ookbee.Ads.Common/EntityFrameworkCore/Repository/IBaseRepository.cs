using Microsoft.EntityFrameworkCore;
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
        bool Any(Expression<Func<TEntity, bool>> filter);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        int Count(Expression<Func<TEntity, bool>> filter = null);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        void Delete(params object[] keyValues);
        Task DeleteAsync(params object[] keyValues);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int start = 0, int length = 20, bool disableTracking = true);
        IEnumerable<TResult> Find<TResult>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int start = 0, int length = 20, bool disableTracking = true) where TResult : class;
        IEnumerable<TResult> Find<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int start = 0, int length = 20, bool disableTracking = true) where TResult : class;
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int start = 0, int length = 20, bool disableTracking = true);
        Task<IEnumerable<TResult>> FindAsync<TResult>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int start = 0, int length = 20, bool disableTracking = true) where TResult : class;
        Task<IEnumerable<TResult>> FindAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int start = 0, int length = 20, bool disableTracking = true) where TResult : class;
        TEntity First(params object[] keyValues);
        TEntity First(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);
        Task<TEntity> FirstAsync(params object[] keyValues);
        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);
        Task<TResult> FirstAsync<TResult>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);
        Task<TResult> FirstAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);
        DbSet<TEntity> GetDbSet();
        void Insert(TEntity entity);
        Task InsertAsync(TEntity entity);
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        decimal Sum(Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> filter = null);
        Task<decimal> SumAsync(Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> filter = null);
        void Update(object id, TEntity entity);
        void Update(object id, TEntity entity, params Expression<Func<TEntity, object>>[] properties);
        Task UpdateAsync(object id, TEntity entity);
        Task UpdateAsync(object id, TEntity entity, params Expression<Func<TEntity, object>>[] properties);
    }
}
