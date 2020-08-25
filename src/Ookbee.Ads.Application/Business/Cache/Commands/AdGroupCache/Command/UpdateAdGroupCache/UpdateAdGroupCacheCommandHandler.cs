﻿using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupById;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.UpdateAdUnitCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.UpdateAdGroupCache
{
    public class UpdateAdGroupCacheCommandHandler : IRequestHandler<UpdateAdGroupCacheCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public UpdateAdGroupCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(UpdateAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdGroupById = await Mediator.Send(new GetAdGroupByIdQuery(request.AdGroupId), cancellationToken);
            if (getAdGroupById.Ok)
            {
                var adUnit = getAdGroupById.Data;
                var redisKey = CacheKey.Groups();
                var hashField = adUnit.Id;
                var hashValue = JsonHelper.Serialize(adUnit);
                await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);
            }

            return Unit.Value;
        }
    }
}