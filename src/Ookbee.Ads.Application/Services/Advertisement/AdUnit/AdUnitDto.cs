using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit
{
    public class AdUnitDto : DefaultDto
    {
        public AdGroupDto AdGroup { get; set; }
        public AdUnitNetworkDto AdNetwork { get; set; }
        public int? SortSeq { get; set; }
    }
}