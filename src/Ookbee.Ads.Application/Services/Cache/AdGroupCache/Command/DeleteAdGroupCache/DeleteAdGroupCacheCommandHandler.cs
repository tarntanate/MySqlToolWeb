﻿using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.DeleteAdUnitCache;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdGroupCache.Commands.DeleteAdGroupCache
{
    public class DeleteAdGroupCacheCommandHandler : IRequestHandler<DeleteAdGroupCacheCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public DeleteAdGroupCacheCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.Groups();
            var hashField = request.AdGroupId;
            await DeleteAdUnitCache(request.AdGroupId, cancellationToken);
            await AdsRedis.HashDeleteAsync(redisKey, hashField);

            return Unit.Value;
        }

        public async Task DeleteAdUnitCache(long adGroupId, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = true;
            do
            {
                next = false;
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, adGroupId), cancellationToken);
                if (getAdUnitList.IsSuccess &&
                    getAdUnitList.Data.HasValue())
                {
                    var items = getAdUnitList.Data;
                    foreach (var item in items)
                    {
                        await Mediator.Send(new DeleteAdUnitCacheCommand(item.Id), cancellationToken);
                    }
                    start += length;
                    next = items.Count() < length ? false : true;
                }
            }
            while (next);
        }
    }
}
