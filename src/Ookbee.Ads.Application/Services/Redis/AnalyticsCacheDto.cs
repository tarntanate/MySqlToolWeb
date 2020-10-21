using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Redis
{
    public class AnalyticsCacheDto
    {
        public IEnumerable<string> Clicks { get; set; }
        public IEnumerable<string> Impressions { get; set; }
    }
}
