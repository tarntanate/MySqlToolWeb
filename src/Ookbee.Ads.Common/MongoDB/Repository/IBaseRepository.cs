using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Anna.Common.MongoDB
{
    public interface IBaseRepository<TDocument> where TDocument : class
    {
        void Add(TDocument item);
        Task AddAsync(TDocument item);
        void AddRange(IEnumerable<TDocument> items);
        Task AddRangeAsync(IEnumerable<TDocument> items);
        bool Any();
        bool Any(Expression<Func<TDocument, bool>> filter);
        Task<bool> AnyAsync();
        Task<bool> AnyAsync(Expression<Func<TDocument, bool>> filter);
        long Count();
        long Count(Expression<Func<TDocument, bool>> filter);
        Task<long> CountAsync();
        Task<long> CountAsync(Expression<Func<TDocument, bool>> filter);
        void Delete(Expression<Func<TDocument, bool>> filter);
        void Delete(object key);
        Task DeleteAsync(Expression<Func<TDocument, bool>> filter);
        Task DeleteAsync(object key);
        IEnumerable<TDocument> Find(SortDefinition<TDocument> sort = null, int start = 0, int length = 10);
        IEnumerable<TDocument> Find(Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sort = null, int start = 0, int length = 10);
        IEnumerable<TResult> Find<TResult>(Expression<Func<TDocument, TResult>> selector, Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sort = null, int start = 0, int length = 10);
        Task<IEnumerable<TDocument>> FindAsync(SortDefinition<TDocument> sort = null, int start = 0, int length = 10);
        Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sort = null, int start = 0, int length = 10);
        Task<IEnumerable<TResult>> FindAsync<TResult>(Expression<Func<TDocument, TResult>> selector, Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sort = null, int start = 0, int length = 10);
        TDocument FirstOrDefault(Expression<Func<TDocument, bool>> filter);
        Task<TDocument> FirstOrDefaultAsync(Expression<Func<TDocument, bool>> filter);
        TDocument Select(object key);
        Task<TDocument> SelectAsync(object key);
        TDocument SingleOrDefault(Expression<Func<TDocument, bool>> filter);
        Task<TDocument> SingleOrDefaultAsync(Expression<Func<TDocument, bool>> filter);
        void Update(object key, TDocument item);
        Task UpdateAsync(object key, TDocument item);
        Task UpdateManyPartialAsync(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update);
        void UpdatePartial(object key, object item);
        Task UpdatePartialAsync(object key, object item);
    }
}