using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateAdByUnitId
{
    public class CreateAdByUnitIdCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public CreateAdByUnitIdCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
