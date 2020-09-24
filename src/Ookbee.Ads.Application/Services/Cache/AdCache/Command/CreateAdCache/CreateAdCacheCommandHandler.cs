using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdById;
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

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.CreateAdCache
{
    public class CreateAdCacheCommandHandler : IRequestHandler<CreateAdCacheCommand>
    {
        private readonly IMapper Mapper;
        private readonly IMediator Mediator;
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
            if (getAdById.Ok &&
                getAdById.Data.HasValue())
            {
                if (getAdById.Data.Status == AdStatus.Publish ||
                    getAdById.Data.Status == AdStatus.Preview)
                {
                    var ad = Mapper.Map<AdCacheDto>(getAdById.Data);
                    var platforms = getAdById.Data.Platforms;
                    foreach (var platform in platforms)
                    {
                        var obj = PrepareAnalytics(ad, platform);

                        var redisKey = CacheKey.Ad(getAdById.Data.Id);
                        var hashField = platform.ToString();
                        var hashValue = (RedisValue)JsonHelper.Serialize(obj);
                        await AdsRedis.HashSetAsync(redisKey, hashField, hashValue);

                        redisKey = getAdById.Data.Status == AdStatus.Preview
                            ? CacheKey.UnitsAdIdsPreview(getAdById.Data.AdUnit.Id, platform)
                            : CacheKey.UnitsAdIds(getAdById.Data.AdUnit.Id, platform);
                        hashValue = (RedisValue)getAdById.Data.Id;
                        await AdsRedis.SetAddAsync(redisKey, hashValue);
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
