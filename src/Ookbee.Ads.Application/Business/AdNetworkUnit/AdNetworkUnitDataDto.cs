using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetworkUnit
{
    public class AdNetworkUnitDataDto
    {
        public int? CountdownSecond { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public string LinkUrl { get; set; }

        public string UnitType { get; set; }

        public IEnumerable<AdNetworkUnitAssetDto> Assets { get; set; }

        public AdNetworkUnitAnalyticsDto Analytics { get; set; }
    }
}
