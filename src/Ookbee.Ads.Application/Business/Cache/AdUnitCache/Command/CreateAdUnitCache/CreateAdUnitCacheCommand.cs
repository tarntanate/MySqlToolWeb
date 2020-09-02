using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.CreateAdUnitCache
{
    public class CreateAdUnitCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public CreateAdUnitCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
