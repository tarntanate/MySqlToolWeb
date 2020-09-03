using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.UpdateAdCache;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.UpdateAdCacheByAssetId
{
    public class UpdateAdCacheByAssetIdCommandHandler : IRequestHandler<UpdateAdCacheByAssetIdCommand>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public UpdateAdCacheByAssetIdCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(UpdateAdCacheByAssetIdCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdAssetByIdQuery(request.AdId), cancellationToken);
            if (getAdById.Ok)
            {
                await Mediator.Send(new UpdateAdCacheCommand(getAdById.Data.AdId));
            }

            return Unit.Value;
        }
    }
}
