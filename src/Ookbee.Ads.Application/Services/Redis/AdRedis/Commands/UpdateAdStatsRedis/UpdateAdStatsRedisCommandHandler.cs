using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdUnitId;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.ArchiveAdStatsByIdRedis;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.DeleteAdRedis;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.UpdateAdStatsRedis
{
    public class UpdateAdStatsRedisCommandHandler : IRequestHandler<UpdateAdStatsRedisCommand, Response<bool>>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public UpdateAdStatsRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Response<bool>> Handle(UpdateAdStatsRedisCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            var redisKey = RedisKeys.AdStats(request.AdId);
            var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
            if (keyExists)
            {
                var hashField = request.StatsType.ToString();
                var hashValue = request.Value;
                var incrementValue = await AdsRedis.HashIncrementAsync(redisKey, hashField, hashValue);
                if (request.StatsType == AdStatsType.Impression)
                {
                    hashField = AdStatsType.Quota.ToString();
                    var quota = (long)await AdsRedis.HashGetAsync(redisKey, hashField);
                    var impression = incrementValue;
                    if (impression >= quota)
                    {
                        var getAdUnitId = await Mediator.Send(new GetAdUnitIdQuery(request.AdId), cancellationToken);
                        if (getAdUnitId.IsSuccess)
                        {
                            var caculatedAt = MechineDateTime.Date;
                            await Mediator.Send(new ArchiveAdStatsByIdRedisCommand(caculatedAt, request.AdId), cancellationToken);
                            await Mediator.Send(new DeleteAdRedisCommand(caculatedAt, getAdUnitId.Data), cancellationToken);
                        }
                    }
                }
                return response.OK(true);
            }
            return response.NotFound($"Unable to update stats: Invalid or expired data.");
        }
    }
}
