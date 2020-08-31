using Ookbee.Ads.Application.Business.Cache.AdAssetCache;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache
{
    public class AdUnitCacheDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UnitId { get; set; }

        public AdAnalyticsCacheDto Analytics { get; set; }
    }
}
