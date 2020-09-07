﻿using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.UpdateAdCacheByAssetId;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommandHandler : IRequestHandler<UpdateAdAssetCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public UpdateAdAssetCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdAssetCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdAssetEntity>(request);
            await AdAssetDbRepo.UpdateAsync(entity.Id, entity);
            await AdAssetDbRepo.SaveChangesAsync(cancellationToken);
            await Mediator.Send(new UpdateAdCacheByAssetIdCommand(entity.Id), cancellationToken);

            var result = new HttpResult<bool>();
            return result.Success(true);
        }
    }
}