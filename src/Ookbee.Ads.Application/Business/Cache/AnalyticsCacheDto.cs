using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Cache
{
    public class AnalyticsCacheDto
    {
        public long? AdId { get; set; }
        public long? AdUnit { get; set; }
        public List<string> Clicks { get; set; }
        public List<string> Impressions { get; set; }
    }
}
