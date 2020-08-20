using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateUnitListByGroupId
{
    public class CreateUnitListByGroupIdCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public CreateUnitListByGroupIdCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
