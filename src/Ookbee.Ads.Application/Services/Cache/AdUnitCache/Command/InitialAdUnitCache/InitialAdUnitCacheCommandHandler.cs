using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.CreateAdUnitCacheGroupId;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.InitialAdUnitCache
{
    public class InitialAdUnitCacheCommandHandler : IRequestHandler<InitialAdUnitCacheCommand>
    {
        private readonly IMapper Mapper;
        private readonly IMediator Mediator;
        private readonly IDatabase AdsRedis;

        public InitialAdUnitCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(InitialAdUnitCacheCommand request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new CreateAdUnitCacheByGroupIdCommand(request.AdGroupId), cancellationToken);

            return Unit.Value;
        }
    }
}
