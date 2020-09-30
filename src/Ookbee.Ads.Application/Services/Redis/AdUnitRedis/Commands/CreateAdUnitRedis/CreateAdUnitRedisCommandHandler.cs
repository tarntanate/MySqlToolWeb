using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitList;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdRedis;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitStatsRedis;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitRedis
{
    public class CreateAdUnitRedisCommandHandler : IRequestHandler<CreateAdUnitRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public CreateAdUnitRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdUnitRedisCommand request, CancellationToken cancellationToken)
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
                    var items = getAdUnitList.Data;
                    adUnits.AddRange(items);
                    foreach (var adUnit in items)
                    {
                        var redisKey = string.Empty;
                        var redisValue = string.Empty;

                        redisKey = CacheKey.UnitIds();
                        await AdsRedis.SetAddAsync(redisKey, adUnit.Id, CommandFlags.FireAndForget);

                        redisKey = CacheKey.GroupUnitIds(request.AdGroupId);
                        await AdsRedis.SetAddAsync(redisKey, adUnit.Id, CommandFlags.FireAndForget);

                        await Mediator.Send(new CreateAdUnitStatsRedisCommand(request.CaculatedAt, adUnit.Id));
                        await Mediator.Send(new CreateAdRedisCommand(request.CaculatedAt, adUnit.Id));
                    }
                    next = items.Count() == length ? true : false;
                }
            }
            while (next);

            if (adUnits.HasValue())
            {
                var baseUrl = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                var platforms = EnumHelper.GetValues<AdPlatform>().Where(platform => platform != AdPlatform.Unknown);
                foreach (var platform in platforms)
                {
                    var redisValue = adUnits.Select(unit => new
                    {
                        Id = unit?.AdNetwork.Name.ToUpper() == "OOKBEE"
                            ? unit.Id.ToString()
                            : unit
                                ?.AdNetwork
                                    ?.AdNetworkUnits
                                        ?.SingleOrDefault(x => x.Platform == platform)
                                            ?.AdNetworkUnitId,
                        Name = unit.AdNetwork.Name,
                        Analytics = unit?.AdNetwork.Name.ToUpper() == "OOKBEE"
                            ? null
                            : new
                            {
                                Click = new List<string>() {
                                    $"{baseUrl}/api/units/{unit.Id}/stats?platform={platform}&type={AdStatsType.Click}".ToLower()
                                },
                                Impressions = new List<string>() {
                                    $"{baseUrl}/api/units/{unit.Id}/stats?platform={platform}&type={AdStatsType.Impression}".ToLower()
                                },
                            }
                    });
                    var redisKey = CacheKey.GroupUnitPlatforms(request.AdGroupId);
                    var hashField = platform.ToString();
                    var hashValue = JsonHelper.Serialize(redisValue.Where(x => !string.IsNullOrEmpty(x.Id)));
                    await AdsRedis.HashSetAsync(redisKey, hashField, hashValue, When.Always, CommandFlags.FireAndForget);
                }
            }

            return Unit.Value;
        }
    }
}
