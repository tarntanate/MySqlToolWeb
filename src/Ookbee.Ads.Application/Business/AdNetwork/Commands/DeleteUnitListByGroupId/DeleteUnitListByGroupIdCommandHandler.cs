using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteAdByUnitId;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteUnitListByGroupId
{
    public class DeleteUnitListByGroupIdCommandHandler : IRequestHandler<DeleteUnitListByGroupIdCommand, Unit>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public DeleteUnitListByGroupIdCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(DeleteUnitListByGroupIdCommand request, CancellationToken cancellationToken)
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
                    await Mediator.Send(new DeleteAdByUnitIdCommand(unit.Id));
                }
            }

            return Unit.Value;
        }
    }
}
