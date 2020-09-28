﻿using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.UpdateAdCache;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.CeateAdCacheByAssetId
{
    public class CreateAdCacheByAssetIdCommandHandler : IRequestHandler<CreateAdCacheByAssetIdCommand>
    {
        private readonly IMediator Mediator;

        public CreateAdCacheByAssetIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(CreateAdCacheByAssetIdCommand request, CancellationToken cancellationToken)
        {
            var getAdAssetById = await Mediator.Send(new GetAdAssetByIdQuery(request.AdAssetId), cancellationToken);
            if (getAdAssetById.IsSuccess)
            {
                await Mediator.Send(new UpdateAdCacheCommand(getAdAssetById.Data.AdId));
            }

            return Unit.Value;
        }
    }
}
