﻿using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Services.Cache.AdUnitCache.Commands.UpdateAdUnitCache;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.UpdateAdUnit
{
    public class UpdateAdUnitCommandHandler : IRequestHandler<UpdateAdUnitCommand, Response<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public UpdateAdUnitCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdUnitCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdUnitEntity>(request);
            await AdUnitDbRepo.UpdateAsync(entity.Id, entity);
            await AdUnitDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new UpdateAdUnitCacheCommand(entity.AdGroupId), cancellationToken);
            return new Response<bool>().Success(true);
        }
    }
}
