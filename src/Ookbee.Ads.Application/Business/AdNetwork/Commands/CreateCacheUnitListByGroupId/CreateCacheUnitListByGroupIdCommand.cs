using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheUnitListByGroupId
{
    public class CreateCacheUnitListByGroupIdCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public CreateCacheUnitListByGroupIdCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
