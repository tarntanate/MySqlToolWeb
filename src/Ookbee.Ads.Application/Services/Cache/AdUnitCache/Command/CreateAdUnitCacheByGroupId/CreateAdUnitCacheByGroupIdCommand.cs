using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.CreateAdUnitCacheGroupId
{
    public class CreateAdUnitCacheByGroupIdCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public CreateAdUnitCacheByGroupIdCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
