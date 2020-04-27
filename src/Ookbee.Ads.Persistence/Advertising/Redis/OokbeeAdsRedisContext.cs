﻿using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Common.Redis.Context;

namespace Ookbee.Ads.Persistence.Advertising.Redis
{
    public class OokbeeAdsRedisContext : RedisContext
    {
        public override string ConnectionString()
        {
            return GlobalVar.AppSettings.ConnectionStrings.Redis;
        }
    }
}
