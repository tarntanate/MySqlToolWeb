using Ookbee.Ads.Common.Extensions;
using RedisPooler;
using StackExchange.Redis;

namespace Ookbee.Ads.Common.Redis.Context
{
    public abstract class RedisContext
    {
        private ConnectionPool ConnectionPool { get; set; }

        public IDatabase Database(int db = -1, object asyncState = null)
        {
            if (!ConnectionPool.HasValue())
            {
                var config = this.Config();
                var poolSize = config.PoolSize;
                var configurationOptions = config.ConfigurationOptions;
                var useLazyInit = config.UseLazyInit;
                var redisTextWriterLog = config.RedisTextWriterLog;
                ConnectionPool = new RedisPooler.ConnectionPool(poolSize, configurationOptions, useLazyInit, redisTextWriterLog);
            }
            return ConnectionPool.GetConnection().GetDatabase(db, asyncState);
        }

        abstract public RedisConnectionPoolConfig Config();
    }
}
