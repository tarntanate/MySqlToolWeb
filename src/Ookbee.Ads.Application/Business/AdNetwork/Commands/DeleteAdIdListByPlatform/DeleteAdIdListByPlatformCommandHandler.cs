using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteAdById;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Commands.DeleteAdIdListByPlatform
{
    public class DeleteAdIdListByPlatformCommandHandler : IRequestHandler<DeleteAdIdListByPlatformCommand, Unit>
    {
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public DeleteAdIdListByPlatformCommandHandler(
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mediator = mediator;
            AdsRedis = adsRedis.Database(0);
        }

        public async Task<Unit> Handle(DeleteAdIdListByPlatformCommand request, CancellationToken cancellationToken)
        {
            var platforms = Enum.GetValues(typeof(Platform));
            foreach (var platform in platforms)
            {
                var redisKey = CacheKey.AdIdsByUnit(request.AdUnitId, platform.ToString());
                var keyExists = await AdsRedis.KeyExistsAsync(redisKey);
                if (keyExists)
                {
                    var adIds = await AdsRedis.SetMembersAsync(redisKey);
                    await AdsRedis.KeyDeleteAsync(redisKey);
                    foreach (var adId in adIds)
                    {
                        await Mediator.Send(new DeleteAdByIdCommand((long)adId));
                    }
                }
            }

            return Unit.Value;
        }
    }
}
