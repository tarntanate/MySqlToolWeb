using Ookbee.Ads.Common.Extensions;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

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

        public async Task FlushDatabase(int db = -1, CommandFlags commandFlags = CommandFlags.None)
        {
            var endPoints = Connection.GetEndPoints();
            foreach (var endPoint in endPoints)
            {
                var server = Connection.GetServer(endPoint);
                if (db == -1)
                    await server.FlushAllDatabasesAsync();
                else
                    await server.FlushDatabaseAsync(db, commandFlags);
            }
        }

        abstract public string ConnectionString();
    }
}