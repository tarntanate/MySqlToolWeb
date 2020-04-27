using System;
using StackExchange.Redis;

namespace Ookbee.Ads.Common.Redis.Context
{
    public interface IRedisContext
    {
        ConnectionMultiplexer Connection { get; }
        IDatabase Database(int db = -1, object asyncState = null);
        Lazy<ConnectionMultiplexer> LazyConnectionMultiplexer();
    }
}