using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetworkItem
{
    public class AdNetworkItemDataDto
    {
        public int? CountdownSecond { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public string LinkUrl { get; set; }

        public string UnitType { get; set; }

        public IEnumerable<AdNetworkItemAssetDto> Assets { get; set; }

        public AdNetworkItemAnalyticsDto Analytics { get; set; }
    }
}
