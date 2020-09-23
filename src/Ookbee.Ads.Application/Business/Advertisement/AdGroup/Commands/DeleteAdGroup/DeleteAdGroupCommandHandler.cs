﻿using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdGroupCache.Commands.DeleteAdGroupCache;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Commands.DeleteAdGroup
{
    public class DeleteAdGroupCommandHandler : IRequestHandler<DeleteAdGroupCommand, Response<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public DeleteAdGroupCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteAdGroupCommand request, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteAdGroupCacheCommand(request.Id), cancellationToken);
            await AdGroupDbRepo.DeleteAsync(request.Id);
            await AdGroupDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
