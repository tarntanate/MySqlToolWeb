﻿using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.DeleteAdCache;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Commands.DeleteAd
{
    public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public DeleteAdCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mediator = mediator;
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();

            await AdDbRepo.DeleteAsync(request.Id);
            await AdDbRepo.SaveChangesAsync(cancellationToken);

            await Mediator.Send(new DeleteAdCacheCommand(request.Id), cancellationToken);

            return result.Success(true);
        }
    }
}
