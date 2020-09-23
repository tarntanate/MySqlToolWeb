﻿using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.GetAdUnitById;
using Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.DeleteAdUnitCache;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Commands.DeleteAdUnit
{
    public class DeleteAdUnitCommandHandler : IRequestHandler<DeleteAdUnitCommand, Response<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public DeleteAdUnitCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteAdUnitCommand request, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();

            var getAdUnitById = await Mediator.Send(new GetAdUnitByIdQuery(request.Id), cancellationToken);
            if (!getAdUnitById.Ok)
                return result.Fail(getAdUnitById.StatusCode, getAdUnitById.Message);

            await Mediator.Send(new DeleteAdUnitCacheCommand(getAdUnitById.Data.Id), cancellationToken);
            await AdUnitDbRepo.DeleteAsync(request.Id);
            await AdUnitDbRepo.SaveChangesAsync(cancellationToken);

            return result.Success(true);
        }
    }
}
