using MediatR;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAdUnitIdCache
{
    public class CreateAdUnitIdCacheCommand : IRequest<Unit>
    {
        public List<long> AdUnitIds { get; private set; } = new List<long>();
        
        public CreateAdUnitIdCacheCommand(long adUnitId)
        {
            AdUnitIds.Add(adUnitId);
        }
        
        public CreateAdUnitIdCacheCommand(IEnumerable<long> adUnitIds)
        {
            AdUnitIds.AddRange(adUnitIds);
        }
    }
}
