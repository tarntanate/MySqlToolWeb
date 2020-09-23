using MediatR;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.CreateAdCache
{
    public class CreateAdCacheCommand : IRequest<Unit>
    {
        public long AdId { get; set; }

        public CreateAdCacheCommand(long adId)
        {
            AdId = adId;
        }
    }
}
