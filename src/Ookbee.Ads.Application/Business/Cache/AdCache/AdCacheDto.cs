using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache
{
    public class AdAssetCacheDto
    {
        public int? CountdownSecond { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public string LinkUrl { get; set; }

        public string UnitType { get; set; }

        public IEnumerable<AssetCacheDto> Assets { get; set; }

        public AnalyticsCacheDto Analytics { get; set; }

    }
}
