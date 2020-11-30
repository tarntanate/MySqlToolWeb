using StackExchange.Redis;
using System.IO;

namespace Ookbee.Ads.Common.Redis.Context
{
    public class RedisConnectionPoolConfig
    {
        public ConfigurationOptions ConfigurationOptions { get; set; }
        public bool UseLazyInit { get; set; }
        public TextWriter RedisTextWriterLog { get; set; }
        public int PoolSize { get; set; }
    }
}