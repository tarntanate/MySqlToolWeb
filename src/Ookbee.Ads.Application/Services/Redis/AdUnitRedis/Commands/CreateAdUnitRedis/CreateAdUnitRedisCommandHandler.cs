using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitIdList;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdFillRateRedis;
using Ookbee.Ads.Application.Services.Redis.AdRedis.Commands.CreateAdRedis;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitByPlatformRedis;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitIdRedis;
using Ookbee.Ads.Application.Services.Redis.AdUnitRedis.Commands.CreateAdUnitStatsRedis;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
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
            do
            {
                var getAdUnitIdList = await Mediator.Send(new GetAdUnitIdListQuery(start, length, request.AdGroupId), cancellationToken);
                if (getAdUnitIdList.IsSuccess)
                {
                    var adUnitIds = getAdUnitIdList.Data;
                    foreach (var adUnitId in adUnitIds)
                    {
                        await Mediator.Send(new CreateAdUnitIdRedisCommand(adUnitId));
                        await Mediator.Send(new CreateAdUnitByPlatformRedisCommand(request.AdGroupId));
                        await Mediator.Send(new CreateAdUnitStatsRedisCommand(request.CaculatedAt, adUnitId));
                        await Mediator.Send(new CreateAdRedisCommand(request.CaculatedAt, adUnitId));
                        await Mediator.Send(new CreateAdFillRateRedisCommand(request.CaculatedAt, adUnitId), cancellationToken);
                    }
                    next = adUnitIds.Count() == length ? true : false;
                }
            }
            while (next);

            return Unit.Value;
        }
    }
}
