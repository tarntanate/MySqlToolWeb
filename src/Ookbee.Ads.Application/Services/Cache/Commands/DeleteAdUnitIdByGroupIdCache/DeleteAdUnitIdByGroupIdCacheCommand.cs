using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdUnitIdByGroupIdCache
{
    public class DeleteAdUnitIdByGroupIdCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }

        public DeleteAdUnitIdByGroupIdCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
