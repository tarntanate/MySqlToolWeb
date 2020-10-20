namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis
{
    public class AdUnitCacheDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public AdUnitStatsCacheDto Analytics { get; set; }
    }
}
