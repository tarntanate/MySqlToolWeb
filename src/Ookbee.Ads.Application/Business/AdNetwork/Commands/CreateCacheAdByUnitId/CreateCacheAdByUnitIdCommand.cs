using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheAdByUnitId
{
    public class CreateCacheAdByUnitIdCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public CreateCacheAdByUnitIdCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
