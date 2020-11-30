namespace Ookbee.Ads.Infrastructure.Settings
{
    public class RedisSettings
    {
        public int PoolSize { get; set; }
        public string EndPoint { get; set; }
        public string Password { get; set; }
        public int TimeoutMS { get; set; }
        public bool UseLazyInit { get; set; }
    }
}
