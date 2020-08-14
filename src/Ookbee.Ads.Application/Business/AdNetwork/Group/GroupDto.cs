using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.Group
{
    public class GroupDto
    {
        public IEnumerable<GroupUnitDto> AdUnits { get; set; }

        public IEnumerable<GroupAnalyticsDto> Analytics { get; set; }
    }
}
