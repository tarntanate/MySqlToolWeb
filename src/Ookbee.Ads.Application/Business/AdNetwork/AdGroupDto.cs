using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork
{
    public class AdNetworkGroupDto
    {
        public IEnumerable<AdNetworkGroupUnitDto> AdUnits { get; set; }

        public IEnumerable<AdNetworkGroupAnalyticsDto> Analytics { get; set; }
    }
}
