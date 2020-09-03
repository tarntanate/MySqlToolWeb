using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.CreateAdAssetCache
{
    public class CreateAdAssetCacheCommandHandler : IRequestHandler<CreateAdAssetCacheCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdAssetCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdAssetCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdById.Ok)
            {
                var ad = getAdById.Data;
                if (ad.Status == AdStatus.Publish ||
                    ad.Status == AdStatus.Preview)
                {
                    var adCache = Mapper.Map<AdCacheDto>(ad);
                    var platforms = getAdById.Data.Platforms;
                    foreach (var platform in platforms)
                    {
                        var queryName = "platform";
                        var queryValue = platform.ToString().ToLower();
                        var baseUri = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                        if (adCache.Analytics.Clicks.HasValue())
                            adCache.Analytics.Clicks = adCache.Analytics.Clicks.Select(url =>
                            {
                                if (url.Contains(baseUri))
                                    url = new Uri(url).AddQueryString(queryName, queryValue).AbsoluteUri;
                                return url;
                            }).ToList();

                        if (adCache.Analytics.Impressions.HasValue())
                            adCache.Analytics.Impressions = adCache.Analytics.Impressions.Select(url =>
                            {
                                if (url.Contains(baseUri))
                                    url = new Uri(url).AddQueryString(queryName, queryValue).AbsoluteUri;
                                return url;
                            }).ToList();

                        var redisKey = CacheKey.Ad(ad.Id, platform);
                        var redisValue = (RedisValue)JsonHelper.Serialize(adCache);
                        await AdsRedis.StringSetAsync(redisKey, redisValue);

                        redisKey = CacheKey.UnitsAdIds(ad.AdUnit.Id, platform);
                        redisValue = (RedisValue)ad.Id;
                        await AdsRedis.SetAddAsync(redisKey, redisValue);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
