using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Cache
{
    public class AnalyticsCacheDto
    {
        public IEnumerable<string> Clicks { get; set; }
        public IEnumerable<string> Impressions { get; set; }
    }
}
