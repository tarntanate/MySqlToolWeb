using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.Commands.CreateAdUnitIdByPlatformCache
{
    public class CreateAdUnitIdByPlatformCacheCommandHandler : IRequestHandler<CreateAdUnitIdByPlatformCacheCommand>
    {
        private readonly IDatabase AdsRedis;
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public CreateAdUnitIdByPlatformCacheCommandHandler(
            AdsRedisContext adsRedis,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdsRedis = adsRedis.Database();
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Unit> Handle(CreateAdUnitIdByPlatformCacheCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            do
            {
                next = false;
                var predicate = AdUnitCacheFilter.Available();
                var items = await AdUnitDbRepo.FindAsync(
                    filter: predicate,
                    orderBy: f => f.OrderBy(x => x.SortSeq),
                    selector: f => new
                    {
                        Id = f.Id,
                        AdGroupId = f.AdGroupId,
                        PublisherId = f.AdGroup.Publisher.Id,
                        AdNetwork = f.AdNetwork,
                        AdNetworks = f.AdNetworks
                    },
                    start: start,
                    length: length
                );
                if (items.HasValue())
                {
                    var adGroupIds = items.Select(x => x.AdGroupId).Distinct();
                    foreach (var adGroupId in adGroupIds)
                    {
                        var baseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                        var platforms = EnumHelper.GetValues<AdPlatform>().Where(platform => platform != AdPlatform.Unknown);
                        foreach (var platform in platforms)
                        {
                            var obj = items.Where(item => item.AdGroupId == adGroupId).Select(item => new AdUnitCacheDto()
                            {
                                Id = item.AdNetwork.ToUpper() == "OOKBEE"
                                    ? item.Id.ToString()
                                    : item.AdNetworks?.FirstOrDefault(f => f.Platform == platform)?.AdNetworkUnitId,
                                Analytics = item.AdNetwork.ToUpper() == "OOKBEE"
                                    ? null
                                    : new AnalyticsCacheDto()
                                    {
                                        Clicks = new List<string>() { $"{baseUrl}/api/units/{item.Id}/stats?type={AdStatsType.Click}&platform={platform}&publisherId={item.PublisherId}".ToLower() },
                                        Impressions = new List<string>() { $"{baseUrl}/api/units/{item.Id}/stats?type={AdStatsType.Impression}&platform={platform}&publisherId={item.PublisherId}".ToLower() }
                                    },
                                Name = item.AdNetwork
                            });
                            var cacheObj = new ApiListResult<AdUnitCacheDto>();
                            cacheObj.Data.Items = obj.ToList();
                            var hashField = platform.ToString();
                            var hashValue = JsonHelper.Serialize(cacheObj);
                            var redisKey = CacheKey.UnitListByPlatform(adGroupId);
                            await AdsRedis.HashSetAsync(redisKey, hashField, hashValue, When.Always, CommandFlags.FireAndForget);
                        }
                    }

                    next = items.Count() == length ? true : false;
                    start += length;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
