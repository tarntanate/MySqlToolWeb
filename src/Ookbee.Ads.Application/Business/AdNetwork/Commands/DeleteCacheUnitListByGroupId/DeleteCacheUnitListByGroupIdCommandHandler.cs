using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheAdByUnitId;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteCacheUnitListByGroupId
{
    public class DeleteCacheUnitListByGroupIdCommandHandler : IRequestHandler<DeleteCacheUnitListByGroupIdCommand, Unit>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public DeleteCacheUnitListByGroupIdCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(DeleteCacheUnitListByGroupIdCommand request, CancellationToken cancellationToken)
        {
            var redisKey = CacheKey.UnitsByGroup(request.AdGroupId);
            var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
            if (keyExists)
            {
                var json = await AdsRedis.StringGetAsync(redisKey);
                await AdsRedis.KeyDeleteAsync(redisKey);
                var units = JsonHelper.Deserialize<IEnumerable<AdNetworkGroupUnitDto>>(json);
                foreach (var unit in units)
                {
                    await Mediator.Send(new DeleteCacheAdByUnitIdCommand(unit.Id));
                }
            }

            return Unit.Value;
        }
    }
}
