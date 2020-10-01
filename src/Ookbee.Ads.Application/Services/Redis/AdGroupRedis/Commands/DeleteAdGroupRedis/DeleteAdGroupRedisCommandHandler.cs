using MediatR;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupById;
using Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.GetAdGroupIdListRedis;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.DeleteAdUnitRedis;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Redis.AdGroupRedis.Commands.DeleteAdGroupRedis
{
    public class DeleteAdGroupRedisCommandHandler : IRequestHandler<DeleteAdGroupRedisCommand>
    {
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public DeleteAdGroupRedisCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(DeleteAdGroupRedisCommand request, CancellationToken cancellationToken)
        {
            var getAdGroupIdList = await Mediator.Send(new GetAdGroupIdListRedisQuery(), cancellationToken);
            if (getAdGroupIdList.IsSuccess)
            {
                var adGroupIds = getAdGroupIdList.Data;
                var redisKey = string.Empty;
                foreach (var adGroupId in adGroupIds)
                {
                    var isExistsAdGroupById = await Mediator.Send(new IsExistsAdGroupByIdQuery(adGroupId));
                    if (isExistsAdGroupById.IsFail)
                    {
                        redisKey = CacheKey.GroupStats(adGroupId);
                        await AdsRedis.KeyDeleteAsync(redisKey);

                        redisKey = CacheKey.GroupUnitIds(adGroupId);
                        await AdsRedis.SetRemoveAsync(redisKey, adGroupId, CommandFlags.FireAndForget);

                        redisKey = CacheKey.GroupUnitPlatforms(adGroupId);
                        await AdsRedis.KeyDeleteAsync(redisKey);

                        redisKey = CacheKey.GroupIds();
                        await AdsRedis.SetRemoveAsync(redisKey, adGroupId, CommandFlags.FireAndForget);
                    }
                    await Mediator.Send(new DeleteAdUnitRedisCommand(adGroupId));
                }
            }

            return Unit.Value;
        }
    }
}
