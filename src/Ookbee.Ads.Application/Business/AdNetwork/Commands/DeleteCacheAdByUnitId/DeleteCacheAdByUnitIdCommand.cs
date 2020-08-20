using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheAdByUnitId
{
    public class DeleteCacheAdByUnitIdCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public DeleteCacheAdByUnitIdCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
