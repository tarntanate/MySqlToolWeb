using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.Commands.DeleteAdGroupUnitIdKeyCache
{
    public class DeleteAdGroupUnitIdKeyCacheCommand : IRequest<Unit>
    {
        public long AdGroupId { get; private set; }

        public DeleteAdGroupUnitIdKeyCacheCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
