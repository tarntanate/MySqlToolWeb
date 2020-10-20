using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis
{
    public class AdUnitStatsCacheDto
    {
        public IEnumerable<string> Clicks { get; set; }
        public IEnumerable<string> Impressions { get; set; }
    }
}
