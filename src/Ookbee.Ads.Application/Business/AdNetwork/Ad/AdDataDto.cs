using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdByKeyQuery
{
    public class AdDataDto
    {
        public int? CountdownSecond { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public string LinkUrl { get; set; }

        public IEnumerable<AdAssetDto> Assets { get; set; }
    }
}
