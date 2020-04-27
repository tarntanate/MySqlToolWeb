using Ookbee.Ads.Common.Extensions;
using StackExchange.Redis;
using System;

namespace Ookbee.Ads.Common.Redis.Context
{
    public abstract class RedisContext : IRedisContext
    {
        private Lazy<ConnectionMultiplexer> ConnectionMultiplexer { get; set; }

        public Lazy<ConnectionMultiplexer> LazyConnectionMultiplexer()
        {
            if (!ConnectionMultiplexer.HasValue())
                ConnectionMultiplexer = new Lazy<ConnectionMultiplexer>(() => StackExchange.Redis.ConnectionMultiplexer.Connect(ConnectionString()));
            return ConnectionMultiplexer;
        }

        public ConnectionMultiplexer Connection => LazyConnectionMultiplexer().Value;

        public IDatabase Database(int db = -1, object asyncState = null)
        {
            return Connection.GetDatabase(db, asyncState);
        }

        abstract public string ConnectionString();
    }
}