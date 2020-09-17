using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Ookbee.Ads.Common.EntityFrameworkCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Common.EntityFrameworkCore.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        public BaseRepository(DbContext context)
        {
            DbContext = context ?? throw new ArgumentException(nameof(context));
            DbSet = DbContext.Set<TEntity>();
        }

        public DbSet<TEntity> GetDbSet()
        {
            return DbSet;
        }

        /// <summary>
        /// determines whether any element of a sequence exists or satisfies a condition.
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>number of rows</returns>
        public virtual bool Any(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                return query.Where(filter).Any();

            return query.Any();
        }

        /// <summary>
        /// get number of rows in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>number of rows</returns>
        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }

        /// <summary>
        /// sum item in table
        /// </summary>
        /// <param name="field">expression selector</param>
        /// <param name="filter">expression filter</param>
        /// <returns>number of rows</returns>
        public virtual decimal Sum(Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            return query.Select(selector).Sum();
        }

        /// <summary>
        /// get first item in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>entity of <typeparamref name="TEntity"/></returns>
        public TEntity First(params object[] keyValues) => DbSet.Find(keyValues);

        /// <summary>
        /// get first item in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="include">include parameters</param>
        /// <param name="disableTracking">change tracking in entity</param>
        /// <returns>entity of <typeparamref name="TEntity"/></returns>
        public TEntity First(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return orderBy(query).FirstOrDefault();

            return query.FirstOrDefault();
        }

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
        public IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int start = 0,
            int length = 20,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return orderBy(query).Skip(start).Take(length).ToList();

            return query.Skip(start).Take(length).ToList();
        }

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
        public IEnumerable<TResult> Find<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int start = 0,
            int length = 20,
            bool disableTracking = true) where TResult : class
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return orderBy(query).Select(selector).Skip(start).Take(length).ToList();

            return query.Select(selector).Skip(start).Take(length).ToList();
        }

        /// <summary>
        /// insert entity
        /// </summary>
        /// <param name="entity">entity</param>
        public void Insert(TEntity entity) => DbSet.Add(entity);

        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="updates">updated field(s)</param>
        public void Update(object id, TEntity entity)
        {
            var entityExists = First(id);
            if (entityExists == null)
                throw new Exception($"There was a problem updating {typeof(TEntity).Name}({id}), Data not found.");

            var entityUpdate = DbSet.Attach(entityExists);
            foreach (var propertie in entityUpdate.Properties)
            {
                if (!propertie.Metadata.IsKey())
                {
                    var propertyName = propertie.Metadata.PropertyInfo.Name;
                    if (!propertyName.Equals("CreatedAt") && !propertyName.Equals("UpdatedAt"))
                    {
                        var updateValue = entity.GetType().GetProperties().Single(f => f.Name == propertie.Metadata.PropertyInfo.Name).GetValue(entity);
                        var currentValue = propertie.CurrentValue;
                        if (currentValue != updateValue)
                        {
                            propertie.CurrentValue = updateValue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="updates">updated field(s)</param>
        public void Update(object id, TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            var entityExists = First(id);
            if (entityExists == null)
                throw new Exception($"There was a problem updating {typeof(TEntity).Name}({id}), Data not found.");

            var entityUpdate = DbSet.Attach(entityExists);
            var fieldExcludes = new List<string>() { "CreatedAt", "UpdatedAt" };
            var fieldIncludes = new List<string>();
            foreach (var property in properties)
            {
                if (!(property.Body is MemberExpression body))
                {
                    var ubody = property.Body as UnaryExpression;
                    body = ubody.Operand as MemberExpression;
                }
                fieldIncludes.Add(body.Member.Name);
            }

            foreach (var property in entityUpdate.Properties)
            {
                var isKey = property.Metadata.IsKey();
                var name = property.Metadata.PropertyInfo.Name;
                if (!isKey && fieldExcludes.Any(e => e != name) && fieldIncludes.Any(e => e == name))
                {
                    var updateValue = entity.GetType().GetProperties().Single(f => f.Name == property.Metadata.PropertyInfo.Name).GetValue(entity);
                    var currentValue = property.CurrentValue;
                    if (currentValue != updateValue)
                    {
                        property.CurrentValue = updateValue;
                    }
                }
            }
        }

        /// <summary>
        /// delete by id
        /// </summary>
        /// <param name="id">id</param>
        public void Delete(params object[] keyValues)
        {
            var entityExists = First(keyValues);
            if (entityExists == null)
                throw new Exception($"There was a problem deleting {typeof(TEntity).Name}({keyValues}), Data not found.");

            DbSet.Remove(entityExists);
        }

        /// <summary>
        /// save an entity data
        /// </summary>
        public int SaveChanges()
        {
            var entities = DbContext
                .ChangeTracker
                .Entries()
                .Where(x => x.Entity is IBaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        if (entity.Entity is ICreatedAt)
                            ((ICreatedAt)entity.Entity).CreatedAt = MechineDateTime.Now;
                        break;

                    case EntityState.Modified:
                        if (entity.Entity is IUpdatedAt)
                            ((IUpdatedAt)entity.Entity).UpdatedAt = MechineDateTime.Now;
                        break;

                    case EntityState.Deleted:
                        if (entity.Entity is IDeletedAt)
                        {
                            entity.State = EntityState.Unchanged;
                            ((IDeletedAt)entity.Entity).DeletedAt = MechineDateTime.Now;
                        }
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            return DbContext.SaveChanges();
        }

        /// <summary>
        /// determines whether any element of a sequence exists or satisfies a condition.
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>number of rows</returns>
        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                return query.Where(filter).AnyAsync();

            return query.AnyAsync();
        }

        /// <summary>
        /// get number of rows in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>number of rows</returns>
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            return query.CountAsync();
        }

        /// <summary>
        /// sum item in table
        /// </summary>
        /// <param name="selector">expression selector</param>
        /// <param name="filter">expression filter</param>
        /// <returns>number of rows</returns>
        public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, long>> selector, Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            return await query.Select(selector).SumAsync();
        }

        /// <summary>
        /// get first item in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <returns>entity of <typeparamref name="TEntity"/></returns>
        public async Task<TEntity> FirstAsync(params object[] keyValues) => await DbSet.FindAsync(keyValues);

        /// <summary>
        /// get first item in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="include">include parameters</param>
        /// <param name="disableTracking">change tracking in entity</param>
        /// <returns>entity of <typeparamref name="TEntity"/></returns>
        public async Task<TEntity> FirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// get first item in table
        /// </summary>
        /// <param name="filter">expression filter</param>
        /// <param name="order">ordering parameters</param>
        /// <param name="include">include parameters</param>
        /// <param name="disableTracking">change tracking in entity</param>
        /// <returns>entity of <typeparamref name="TEntity"/></returns>
        public async Task<TResult> FirstAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();

            return await query.Select(selector).FirstOrDefaultAsync();
        }

        public async Task<TResult> FirstAsync2<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<TEntity, IEnumerable<TResult>> selectMany = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
                query = query.AsNoTracking();


            if (filter != null)
                query = query.Where(filter);

            if (selectMany != null)
                // query = query.SelectMany(selectMany);
                return query.SelectMany(selectMany).DefaultIfEmpty().FirstOrDefault();


            if (orderBy != null)
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();

            return await query.Select(selector).FirstOrDefaultAsync();
        }

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
        public async Task<IEnumerable<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int start = 0,
            int length = 20,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                query = orderBy(query);

            return await query.Skip(start).Take(length).ToListAsync();
        }

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
        public async Task<IEnumerable<TResult>> FindAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            int start = 0,
            int length = 20,
            bool disableTracking = true) where TResult : class
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
                query = query.AsNoTracking();

            if (include != null)
                query = include(query);

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return await orderBy(query).Select(selector).Skip(start).Take(length).ToListAsync();

            return await query.Select(selector).Skip(start).Take(length).ToListAsync();
        }

        /// <summary>
        /// insert entity
        /// </summary>
        /// <param name="entity">entity</param>
        public async Task InsertAsync(TEntity entity) => await DbSet.AddAsync(entity);

        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="updates">updated field(s)</param>
        public async Task UpdateAsync(object id, TEntity entity)
        {
            var entityExists = await FirstAsync(id);
            if (entityExists == null)
                throw new Exception($"There was a problem updating {typeof(TEntity).Name}({id}), Data not found.");

            var entityUpdate = DbSet.Attach(entityExists);
            foreach (var propertie in entityUpdate.Properties)
            {
                if (!propertie.Metadata.IsKey())
                {
                    var propertyName = propertie.Metadata.PropertyInfo.Name;
                    if (!propertyName.Equals("CreatedAt") && !propertyName.Equals("UpdatedAt"))
                    {
                        var updateValue = entity.GetType().GetProperties().Single(f => f.Name == propertie.Metadata.PropertyInfo.Name).GetValue(entity);
                        var currentValue = propertie.CurrentValue;
                        if (currentValue != updateValue)
                        {
                            propertie.CurrentValue = updateValue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// update an entity
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="updates">updated field(s)</param>
        public async Task UpdateAsync(object id, TEntity entity, params Expression<Func<TEntity, object>>[] properties)
        {
            var entityExists = await FirstAsync(id);
            if (entityExists == null)
                throw new Exception($"There was a problem updating {typeof(TEntity).Name}({id}), Data not found.");

            var entityUpdate = DbSet.Attach(entityExists);
            var fieldExcludes = new List<string>() { "CreatedAt", "UpdatedAt" };
            var fieldIncludes = new List<string>();
            foreach (var property in properties)
            {
                if (!(property.Body is MemberExpression body))
                {
                    var ubody = property.Body as UnaryExpression;
                    body = ubody.Operand as MemberExpression;
                }
                fieldIncludes.Add(body.Member.Name);
            }

            foreach (var property in entityUpdate.Properties)
            {
                var isKey = property.Metadata.IsKey();
                var name = property.Metadata.PropertyInfo.Name;
                if (!isKey && fieldExcludes.Any(e => e != name) && fieldIncludes.Any(e => e == name))
                {
                    var updateValue = entity.GetType().GetProperties().Single(f => f.Name == property.Metadata.PropertyInfo.Name).GetValue(entity);
                    var currentValue = property.CurrentValue;
                    if (currentValue != updateValue)
                    {
                        property.CurrentValue = updateValue;
                    }
                }
            }
        }

        /// <summary>
        /// delete by id
        /// </summary>
        /// <param name="id">id</param>
        public async Task DeleteAsync(params object[] keyValues)
        {
            var entityExists = await FirstAsync(keyValues);
            if (entityExists == null)
                throw new Exception($"There was a problem deleting {typeof(TEntity).Name}({keyValues}), Data not found.");

            DbSet.Remove(entityExists);
        }

        /// <summary>
        /// save an entity data
        /// </summary>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = DbContext
                .ChangeTracker
                .Entries()
                .Where(x => x.Entity is IBaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        if (entity.Entity is ICreatedAt)
                            ((ICreatedAt)entity.Entity).CreatedAt = MechineDateTime.Now;
                        break;

                    case EntityState.Modified:
                        if (entity.Entity is IUpdatedAt)
                            ((IUpdatedAt)entity.Entity).UpdatedAt = MechineDateTime.Now;
                        break;

                    case EntityState.Deleted:
                        if (entity.Entity is IDeletedAt)
                        {
                            entity.State = EntityState.Unchanged;
                            ((IDeletedAt)entity.Entity).DeletedAt = MechineDateTime.Now;
                        }
                        break;

                    default:
                        throw new NotImplementedException();
                }
            }

            return await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
