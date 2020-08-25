using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.GetAdAssetByUnitId
{
    public class GetAdAssetByUnitIdQueryHandler : IRequestHandler<GetAdAssetByUnitIdQuery, HttpResult<string>>
    {
        private IMapper Mapper { get; }
        private IDatabase AdsRedis { get; }

        public GetAdAssetByUnitIdQueryHandler(
            IMapper mapper,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<HttpResult<string>> Handle(GetAdAssetByUnitIdQuery request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.AdIdsByUnit(request.AdUnitId, request.Platform);
            var redisValue = string.Empty;
            var redisValues = await AdsRedis.SetMembersAsync(redisKey);
            if (redisValues.HasValue())
            {
                var adIds = redisValues.Select(x => (long)x);
                var adId = adIds.OrderBy(id => Guid.NewGuid()).First();
                redisKey = CacheKey.Ad(adId);
                redisValue = await AdsRedis.StringGetAsync(redisKey);
            }

            var result = new HttpResult<string>();
            return result.Success(redisValue);
        }
    }
}
