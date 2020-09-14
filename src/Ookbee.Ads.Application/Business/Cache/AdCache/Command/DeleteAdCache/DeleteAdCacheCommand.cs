using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.DeleteAdCache
{
    public class DeleteAdCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public DeleteAdCacheCommand(long adId)
        {
            AdId = adId;
        }
    }
}
