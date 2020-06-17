using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Common.Redis.Context;

namespace Ookbee.Ads.Persistence.Redis.AdsRedis
{
    public class AdsRedisContext : RedisContext
    {
        public override string ConnectionString()
        {
            return GlobalVar.AppSettings.ConnectionStrings.Redis.Ads;
        }
    }
}
