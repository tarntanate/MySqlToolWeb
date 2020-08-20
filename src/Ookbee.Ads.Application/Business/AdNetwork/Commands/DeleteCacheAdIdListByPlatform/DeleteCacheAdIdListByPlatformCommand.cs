using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheAdIdListByPlatform
{
    public class DeleteCacheAdIdListByPlatformCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public DeleteCacheAdIdListByPlatformCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
