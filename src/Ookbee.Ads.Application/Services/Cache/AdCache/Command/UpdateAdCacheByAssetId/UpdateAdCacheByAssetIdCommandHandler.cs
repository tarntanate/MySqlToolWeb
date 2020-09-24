using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.UpdateAdCache;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.UpdateAdCacheByAssetId
{
    public class UpdateAdCacheByAssetIdCommandHandler : IRequestHandler<UpdateAdCacheByAssetIdCommand>
    {
        private readonly IMediator Mediator;

        public UpdateAdCacheByAssetIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateAdCacheByAssetIdCommand request, CancellationToken cancellationToken)
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
