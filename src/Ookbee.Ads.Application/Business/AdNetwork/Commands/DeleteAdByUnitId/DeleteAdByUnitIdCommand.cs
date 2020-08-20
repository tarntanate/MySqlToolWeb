using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteAdByUnitId
{
    public class DeleteAdByUnitIdCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public DeleteAdByUnitIdCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
