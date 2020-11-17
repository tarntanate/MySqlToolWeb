using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache
{
    public class AdUnitAnalyticsCacheDto
    {
        public IEnumerable<string> Clicks { get; set; }
        public IEnumerable<string> Impressions { get; set; }
    }
}
