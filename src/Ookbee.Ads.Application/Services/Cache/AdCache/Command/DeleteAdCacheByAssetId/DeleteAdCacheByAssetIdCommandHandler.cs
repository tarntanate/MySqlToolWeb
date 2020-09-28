using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Services.Cache.AdCache.Commands.UpdateAdCache;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Cache.AdCache.Commands.DeleteAdCacheByAssetId
{
    public class DeleteAdCacheByAssetIdCommandHandler : IRequestHandler<DeleteAdCacheByAssetIdCommand>
    {
        private readonly IMediator Mediator;

        public DeleteAdCacheByAssetIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(DeleteAdCacheByAssetIdCommand request, CancellationToken cancellationToken)
        {
            var getAdById = await Mediator.Send(new GetAdAssetByIdQuery(request.AdAssetId), cancellationToken);
            if (getAdById.IsSuccess)
            {
                await Mediator.Send(new UpdateAdCacheCommand(getAdById.Data.AdId));
            }

            return Unit.Value;
        }
    }
}
