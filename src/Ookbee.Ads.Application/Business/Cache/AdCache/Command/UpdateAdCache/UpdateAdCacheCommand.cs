using MediatR;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.UpdateAdCache
{
    public class UpdateAdCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public UpdateAdCacheCommand(long adId)
        {
            AdId = adId;
        }
    }
}
