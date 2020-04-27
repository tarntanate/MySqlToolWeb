using MongoDB.Driver;

namespace Anna.Common.MongoDB
{
    public class BaseContext : IBaseContext
    {
        protected BaseContext(string connectionString)
        {
            Database = new MongoClient(connectionString).GetDatabase(new MongoUrl(connectionString).DatabaseName);
        }

        public IMongoDatabase Database { get; }
    }
}