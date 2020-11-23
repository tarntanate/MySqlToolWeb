using MediatR;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAdGroupIdCache
{
    public class CreateAdGroupIdCacheCommand : IRequest<Unit>
    {
        public List<long> AdGroupIds { get; private set; } = new List<long>();
        
        public CreateAdGroupIdCacheCommand(long adGroupId)
        {
            AdGroupIds.Add(adGroupId);
        }
        
        public CreateAdGroupIdCacheCommand(IEnumerable<long> adGroupIds)
        {
            AdGroupIds.AddRange(adGroupIds);
        }
    }
}
