using MongoDB.Driver;

namespace Anna.Common.MongoDB
{
    public interface IBaseContext
    {
        IMongoDatabase Database { get; }
    }
}