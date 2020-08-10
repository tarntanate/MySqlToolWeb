using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdByKeyQuery
{
    public class AdAnalyticsDto
    {
        public List<string> Clicks { get; set; }

        public List<string> Impressions { get; set; }
    }
}
