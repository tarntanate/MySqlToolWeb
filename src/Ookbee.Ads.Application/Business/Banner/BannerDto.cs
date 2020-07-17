using System.Collections.Generic;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerDto
    {
        public BannerAdDto Ad { get; set; }

        public IEnumerable<AdNetwork> AdNetworks { get; set; }

        public string AdUnitType { get; set; }
    }
}
