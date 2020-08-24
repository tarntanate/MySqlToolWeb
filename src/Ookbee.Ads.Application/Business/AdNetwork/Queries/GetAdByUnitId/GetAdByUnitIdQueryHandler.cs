﻿using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Queries.GetAdByUnitId
{
    public class GetAdByUnitIdQueryHandler : IRequestHandler<GetAdByUnitIdQuery, HttpResult<string>>
    {
        private AdsRedisContext AdsRedis { get; }

        public GetAdByUnitIdQueryHandler(AdsRedisContext adsRedis)
        {
            AdsRedis = adsRedis;
        }

        public async Task<HttpResult<string>> Handle(GetAdByUnitIdQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<string>();

            var adId = await RadomAd(request.AdUnitId, request.Platform);
            var redisKey = CacheKey.Ad(adId);
            var redisValue = await AdsRedis.Database(1).StringGetAsync(redisKey);

            if (redisValue.HasValue)
                return result.Success(redisValue);

            return result.Fail(404, $"Data not found.");
        }

        private async Task<long> RadomAd(long adUnitId, string platform)
        {
            var redisKey = CacheKey.AdIdsByUnit(adUnitId, platform);
            var redisValue = await AdsRedis.Database(2).SetRandomMemberAsync(redisKey);
            return (long)redisValue;
        }
    }
}
