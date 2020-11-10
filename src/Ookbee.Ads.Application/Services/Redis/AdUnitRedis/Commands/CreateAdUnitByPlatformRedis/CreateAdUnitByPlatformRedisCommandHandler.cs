using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitByPlatformRedis
{
    public class CreateAdUnitByPlatformRedisCommandHandler : IRequestHandler<CreateAdUnitByPlatformRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdUnitByPlatformRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdUnitByPlatformRedisCommand request, CancellationToken cancellationToken)
        {
            var start = 0;
            var length = 100;
            var next = false;
            var adUnits = new List<AdUnitDto>();
            do
            {
                var getAdUnitList = await Mediator.Send(new GetAdUnitListQuery(start, length, request.AdGroupId), cancellationToken);
                if (getAdUnitList.IsSuccess)
                {
                    adUnits.AddRange(getAdUnitList.Data);
                    next = adUnits.Count() == length ? true : false;
                }
            }
            while (next);

            if (adUnits.HasValue())
            {
                var baseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                var platforms = EnumHelper.GetValues<AdPlatform>().Where(platform => platform != AdPlatform.Unknown);
                foreach (var platform in platforms)
                {
                    var items = adUnits.Select(unit => new AdUnitCacheDto
                    {
                        Id = unit?.AdNetwork.Name.ToUpper() == "OOKBEE"
                            ? unit.Id.ToString()
                            : unit
                                ?.AdNetwork
                                    ?.AdNetworkUnits
                                        ?.FirstOrDefault(x => x.Platform == platform)
                                            ?.AdNetworkUnitId,
                        Name = unit.AdNetwork.Name,
                        Analytics = unit?.AdNetwork.Name.ToUpper() == "OOKBEE"
                            ? null
                            : new AnalyticsCacheDto
                            {
                                Clicks = new List<string>() {
                                        $"{baseUrl}/api/units/{unit.Id}/stats?type={AdStatsType.Click}&platform={platform}&publisherId={unit.AdGroup.Publisher.Id}".ToLower()
                                },
                                Impressions = new List<string>() {
                                        $"{baseUrl}/api/units/{unit.Id}/stats?type={AdStatsType.Impression}&platform={platform}&publisherId={unit.AdGroup.Publisher.Id}".ToLower()
                                },
                            }
                    }).ToList();

                    if (items.Count() > 0)
                    {
                        var cacheObj = new ApiListResult<AdUnitCacheDto>();
                        cacheObj.Data.Items = items.Where(x => !string.IsNullOrEmpty(x.Id)).ToList();

                        var hashField = platform.ToString();
                        var hashValue = JsonHelper.Serialize(cacheObj);
                        var redisKey = CacheKey.GroupUnitPlatforms(request.AdGroupId);
                        await AdsRedis.HashSetAsync(redisKey, hashField, hashValue, When.Always, CommandFlags.FireAndForget);
                    }
                }
            }

            return Unit.Value;
        }
    }
}
