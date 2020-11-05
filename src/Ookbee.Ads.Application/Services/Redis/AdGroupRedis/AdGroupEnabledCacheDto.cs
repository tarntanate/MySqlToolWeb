namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis
{
    public class AdGroupEnabledCacheDto
    {
        public long Id { get; set; }
        public string Placement { get; set; }
        public bool Enabled { get; set; }
    }
}
