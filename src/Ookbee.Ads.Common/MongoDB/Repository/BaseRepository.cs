using Anna.Common.MongoDB.Domain;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Anna.Common.MongoDB
{
    public class BaseRepository<TDocument> : IBaseRepository<TDocument> where TDocument : class
    {
        private IMongoCollection<TDocument> Collection { get; }

        public BaseRepository(IBaseContext context)
        {
            var collectionName = GetCollectionName();
            Collection = context.Database.GetCollection<TDocument>(collectionName);
        }

        public bool Any()
        {
            return Collection.Find(new BsonDocument()).Any();
        }

        public bool Any(Expression<Func<TDocument, bool>> filter)
        {
            return Collection.Find(filter).Any();
        }

        public Task<bool> AnyAsync()
        {
            return Collection.Find(new BsonDocument()).AnyAsync();
        }

        public Task<bool> AnyAsync(Expression<Func<TDocument, bool>> filter)
        {
            return Collection.Find(filter).AnyAsync();
        }

        public long Count()
        {
            return Collection.CountDocuments(new BsonDocument());
        }

        public long Count(Expression<Func<TDocument, bool>> filter)
        {
            return Collection.CountDocuments(filter);
        }

        public Task<long> CountAsync()
        {
            return Collection.CountDocumentsAsync(new BsonDocument());
        }

        public Task<long> CountAsync(Expression<Func<TDocument, bool>> filter)
        {
            return Collection.CountDocumentsAsync(filter);
        }

        public TDocument FirstOrDefault(Expression<Func<TDocument, bool>> filter)
        {
            return Collection.Find(filter).FirstOrDefault();
        }

        public Task<TDocument> FirstOrDefaultAsync(Expression<Func<TDocument, bool>> filter)
        {
            return Collection.Find(filter).FirstOrDefaultAsync();
        }

        public IEnumerable<TDocument> Find(SortDefinition<TDocument> sort = null, int start = 0, int length = 10)
        {
            return Collection.Find(new BsonDocument()).Sort(sort).Skip(start).Limit(length).ToList();
        }

        public async Task<IEnumerable<TDocument>> FindAsync(SortDefinition<TDocument> sort = null, int start = 0, int length = 10)
        {
            return await Collection.Find(new BsonDocument()).Sort(sort).Skip(start).Limit(length).ToListAsync().ConfigureAwait(false);
        }

        public IEnumerable<TDocument> Find(Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sort = null, int start = 0, int length = 10)
        {
            return Collection.Find(filter).Sort(sort).Skip(start).Limit(length).ToList();
        }

        public async Task<IEnumerable<TDocument>> FindAsync(Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sort = null, int start = 0, int length = 10)
        {
            return await Collection.Find(filter).Sort(sort).Skip(start).Limit(length).ToListAsync().ConfigureAwait(false);
        }

        public IEnumerable<TResult> Find<TResult>(Expression<Func<TDocument, TResult>> selector, Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sort = null, int start = 0, int length = 10)
        {
            return Collection.Find(filter).Sort(sort).Project(selector).Skip(start).Limit(length).ToList();
        }

        public async Task<IEnumerable<TResult>> FindAsync<TResult>(Expression<Func<TDocument, TResult>> selector, Expression<Func<TDocument, bool>> filter, SortDefinition<TDocument> sort = null, int start = 0, int length = 10)
        {
            return await Collection.Find(filter).Sort(sort).Project(selector).Skip(start).Limit(length).ToListAsync().ConfigureAwait(false);
        }

        public TDocument Select(object key)
        {
            return Collection.Find(FilterId(key)).SingleOrDefault();
        }

        public Task<TDocument> SelectAsync(object key)
        {
            return Collection.Find(FilterId(key)).SingleOrDefaultAsync();
        }

        public TDocument SingleOrDefault(Expression<Func<TDocument, bool>> filter)
        {
            return Collection.Find(filter).SingleOrDefault();
        }

        public Task<TDocument> SingleOrDefaultAsync(Expression<Func<TDocument, bool>> filter)
        {
            return Collection.Find(filter).SingleOrDefaultAsync();
        }

        public void Add(TDocument item)
        {
            Collection.InsertOne(item);
        }

        public Task AddAsync(TDocument item)
        {
            return Collection.InsertOneAsync(item);
        }

        public void AddRange(IEnumerable<TDocument> items)
        {
            Collection.InsertMany(items);
        }

        public Task AddRangeAsync(IEnumerable<TDocument> items)
        {
            return Collection.InsertManyAsync(items);
        }

        public void Delete(object key)
        {
            Collection.DeleteOne(FilterId(key));
        }

        public Task DeleteAsync(object key)
        {
            return Collection.DeleteOneAsync(FilterId(key));
        }

        public void Delete(Expression<Func<TDocument, bool>> filter)
        {
            Collection.DeleteMany(filter);
        }

        public Task DeleteAsync(Expression<Func<TDocument, bool>> filter)
        {
            return Collection.DeleteManyAsync(filter);
        }

        public void Update(object key, TDocument item)
        {
            Collection.ReplaceOne(FilterId(key), item);
        }

        public Task UpdateAsync(object key, TDocument item)
        {
            return Collection.ReplaceOneAsync(FilterId(key), item);
        }

        public void UpdatePartial(object key, object item)
        {
            Collection.ReplaceOne(FilterId(key), item as TDocument);
        }

        public Task UpdatePartialAsync(object key, object item)
        {
            return Collection.ReplaceOneAsync(FilterId(key), item as TDocument);
        }

        public Task UpdateManyPartialAsync(Expression<Func<TDocument, bool>> filter, UpdateDefinition<TDocument> update)
        {
            return Collection.UpdateManyAsync(filter, update);
        }

        private static FilterDefinition<TDocument> FilterId(object key)
        {
            return Builders<TDocument>.Filter.Eq("Id", key);
        }

        private static string GetCollectionName()
        {
            return (typeof(TDocument).GetCustomAttributes(typeof(CollectionNameAttribute), true).FirstOrDefault() as CollectionNameAttribute).Name;
        }
    }
}