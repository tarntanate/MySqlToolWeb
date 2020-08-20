using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheAdById
{
    public class DeleteCacheAdByIdCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public DeleteCacheAdByIdCommand(long adId)
        {
            AdId = adId;
        }
    }
}
