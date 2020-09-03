﻿using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.UpdateAdAssetCache;
using Ookbee.Ads.Persistence.Redis.AdsRedis;
using StackExchange.Redis;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.UpdateAdAssetCacheByAssetId
{
    public class UpdateAdAssetCacheByAssetIdCommandHandler : IRequestHandler<UpdateAdAssetCacheByAssetIdCommand, Unit>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private IDatabase AdsRedis { get; }

        public UpdateAdAssetCacheByAssetIdCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsRedisContext adsRedis)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdsRedis = adsRedis.Database();
        }

        public async Task<Unit> Handle(UpdateAdAssetCacheByAssetIdCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdAssetByIdQuery(request.AdId), cancellationToken);
            if (getAdById.Ok)
            {
                await Mediator.Send(new UpdateAdAssetCacheCommand(getAdById.Data.AdId));
            }

            return Unit.Value;
        }
    }
}
