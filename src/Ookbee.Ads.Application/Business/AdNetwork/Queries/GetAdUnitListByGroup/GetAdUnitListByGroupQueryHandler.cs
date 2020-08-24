using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetworkStats.Commands.UpdateGroupStats;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Queries.GetAdUnitListByGroup
{
    public class GetAdUnitListByGroupQueryHandler : IRequestHandler<GetAdUnitListByGroupQuery, HttpResult<string>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public GetAdUnitListByGroupQueryHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<HttpResult<string>> Handle(GetAdUnitListByGroupQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<string>();

            var redisKey = CacheKey.UnitsByGroup(request.AdGroupId);
            var redisValue = await AdsRedis.StringGetAsync(redisKey);

            if (redisValue.HasValue())
            {
                await Mediator.Send(new UpdateGroupStatsCommand(request.AdGroupId, AdStats.Request));
                return result.Success(redisValue);
            }
            return result.Fail(404, $"AdGroup '{request.AdGroupId}' doesn't exist.");
        }
    }
}
