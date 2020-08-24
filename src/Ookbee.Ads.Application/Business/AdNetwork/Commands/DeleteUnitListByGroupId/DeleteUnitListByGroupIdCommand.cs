using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteUnitListByGroupId
{
    public class DeleteUnitListByGroupIdCommand : IRequest<Unit>
    {
        public long AdGroupId { get; set; }

        public DeleteUnitListByGroupIdCommand(long adGroupId)
        {
            AdGroupId = adGroupId;
        }
    }
}
