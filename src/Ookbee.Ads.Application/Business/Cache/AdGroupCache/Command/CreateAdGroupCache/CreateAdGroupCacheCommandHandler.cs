using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.GetAdGroupById;
using Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.CreateAdUnitCache;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.CreateAdGroupCache
{
    public class CreateAdGroupCacheCommandHandler : IRequestHandler<CreateAdGroupCacheCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public CreateAdGroupCacheCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(CreateAdGroupCacheCommand request, CancellationToken cancellationToken)
        {
            var getAdGroupById = await Mediator.Send(new GetAdGroupByIdQuery(request.AdGroupId), cancellationToken);
            if (getAdGroupById.Ok)
            {
                await Mediator.Send(new CreateAdUnitCacheCommand(request.AdGroupId), cancellationToken);
            }

            return Unit.Value;
        }
    }
}
