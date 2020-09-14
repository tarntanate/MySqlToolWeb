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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.CreateAdCache
{
    public class CreateAdCacheCommandHandler : IRequestHandler<CreateAdCacheCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdByIdQuery(request.AdId), cancellationToken);
            if (getAdById.Ok)
            {
                if (getAdById.Data.Status == AdStatus.Publish ||
                    getAdById.Data.Status == AdStatus.Preview)
                {
                    var ad = Mapper.Map<AdCacheDto>(getAdById.Data);
                    var platforms = getAdById.Data.Platforms;
                    foreach (var platform in platforms)
                    {
                        var obj = PrepareAnalytics(ad, platform);

                        var redisKey = CacheKey.Ad(getAdById.Data.Id, platform);
                        var redisValue = (RedisValue)JsonHelper.Serialize(obj);
                        await AdsRedis.StringSetAsync(redisKey, redisValue);

                        redisKey = CacheKey.UnitsAdIds(getAdById.Data.AdUnit.Id, platform);
                        redisValue = (RedisValue)getAdById.Data.Id;
                        await AdsRedis.SetAddAsync(redisKey, redisValue);
                    }
                }
            }

            return Unit.Value;
        }

        private AdCacheDto PrepareAnalytics(AdCacheDto ad, Platform platform)
        {
            if (ad.Analytics.HasValue())
            {
                var name = "platform";
                var value = platform.ToString();
                var baseUri = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;

                if (ad.Analytics.Clicks.HasValue())
                    ad.Analytics.Clicks = ad.Analytics.Clicks.Select(url =>
                    {
                        if (url.Contains(baseUri))
                            url = new Uri(url).AddQueryString(name, value).AbsoluteUri;
                        return url.ToLower();
                    }).ToList();

                if (ad.Analytics.Impressions.HasValue())
                    ad.Analytics.Impressions = ad.Analytics.Impressions.Select(url =>
                    {
                        if (url.Contains(baseUri))
                            url = new Uri(url).AddQueryString(name, value).AbsoluteUri;
                        return url.ToLower();
                    }).ToList();
            }

            return ad;
        }
    }
}
