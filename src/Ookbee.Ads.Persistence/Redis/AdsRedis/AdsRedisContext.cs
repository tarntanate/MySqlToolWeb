using Ookbee.Ads.Common.Redis.Context;
using Ookbee.Ads.Infrastructure;
using StackExchange.Redis;

namespace Ookbee.Ads.Persistence.Redis.AdsRedis
{
    public class AdsRedisContext : RedisContext
    {
        public override RedisConnectionPoolConfig Config()
        {
            var config = new ConfigurationOptions();
            config.EndPoints.Add(GlobalVar.AppSettings.Redis.EndPoint);
            config.Password = GlobalVar.AppSettings.Redis.Password;
            config.ConnectTimeout = GlobalVar.AppSettings.Redis.TimeoutMS;

            var redisConnectionPoolConfig = new RedisConnectionPoolConfig();
            redisConnectionPoolConfig.ConfigurationOptions = config;
            redisConnectionPoolConfig.PoolSize = GlobalVar.AppSettings.Redis.PoolSize;
            redisConnectionPoolConfig.RedisTextWriterLog = null;
            redisConnectionPoolConfig.UseLazyInit = GlobalVar.AppSettings.Redis.UseLazyInit;

            return redisConnectionPoolConfig;
        }
    }
}
