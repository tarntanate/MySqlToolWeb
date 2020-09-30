using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.DeleteAdCache
{
    public class DeleteAdCacheCommandHandler : IRequestHandler<DeleteAdCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public DeleteAdCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdById.IsSuccess &&
                getAdById.Data.HasValue())
            {
                var ad = getAdById.Data;
                foreach (var platform in EnumHelper.GetValues<AdPlatform>())
                {
                    if (platform != AdPlatform.Unknown)
                    {
                        var redisKey = CacheKey.AdPlatforms(request.AdId);
                        var hashField = platform.ToString();
                        await AdsRedis.HashDeleteAsync(redisKey, hashField);

                        redisKey = CacheKey.UnitAdIds(ad.AdUnit.Id, platform);
                        var redisValue = request.AdId;
                        await AdsRedis.SetRemoveAsync(redisKey, redisValue);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
