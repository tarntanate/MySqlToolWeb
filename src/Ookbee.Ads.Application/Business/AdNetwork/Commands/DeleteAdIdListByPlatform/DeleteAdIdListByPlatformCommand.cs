using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteAdIdListByPlatform
{
    public class DeleteAdIdListByPlatformCommand : IRequest<Unit>
    {
        public long AdUnitId { get; set; }

        public DeleteAdIdListByPlatformCommand(long adUnitId)
        {
            AdUnitId = adUnitId;
        }
    }
}
