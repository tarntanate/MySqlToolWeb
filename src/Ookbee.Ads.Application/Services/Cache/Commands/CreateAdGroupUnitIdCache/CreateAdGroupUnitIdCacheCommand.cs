using System.Collections.Generic;
using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAdGroupUnitIdCache
{
    public class CreateAdGroupUnitIdCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }
        public List<long> AdUnitIds { get; private set; } = new List<long>();

        public CreateAdGroupUnitIdCacheCommand(long adGroupId, long adUnitId)
        {
            AdGroupId = adGroupId;
            AdUnitIds.Add(adUnitId);
        }

        public CreateAdGroupUnitIdCacheCommand(long adGroupId, IEnumerable<long> adUnitIds)
        {
            AdGroupId = adGroupId;
            AdUnitIds.AddRange(adUnitIds);
        }
    }
}
