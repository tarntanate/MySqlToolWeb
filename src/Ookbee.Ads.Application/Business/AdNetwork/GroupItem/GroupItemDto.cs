using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.AdNetwork.GroupItem
{
    public class GroupItemDto
    {
        public IEnumerable<GroupItemUnitDto> AdUnits { get; set; }

        public IEnumerable<GroupItemAnalyticsDto> Analytics { get; set; }
    }
}
