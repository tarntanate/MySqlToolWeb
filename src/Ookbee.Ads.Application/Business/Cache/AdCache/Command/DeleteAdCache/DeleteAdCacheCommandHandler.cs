﻿using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.DeleteAdCache
{
    public class DeleteAdCacheCommandHandler : IRequestHandler<DeleteAdCacheCommand>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

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
            if (getAdById.Ok &&
                getAdById.Data.HasValue())
            {
                var ad = getAdById.Data;
                foreach (var platform in EnumHelper.GetValues<Platform>())
                {
                    if (platform != Platform.Unknown)
                    {
                        var redisKey = CacheKey.Ad(request.AdId);
                        var hashField = platform.ToString();
                        await AdsRedis.HashDeleteAsync(redisKey, hashField);

                        redisKey = CacheKey.UnitsAdIds(ad.AdUnit.Id, platform);
                        var redisValue = request.AdId;
                        await AdsRedis.SetRemoveAsync(redisKey, redisValue);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
