using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.UpdateAdCache;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdCache.Commands.UpdateAdCacheByAssetId
{
    public class UpdateAdCacheByAssetIdCommandHandler : IRequestHandler<UpdateAdCacheByAssetIdCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdCacheByAssetIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateAdCacheByAssetIdCommand request, CancellationToken cancellationToken)
        {
            var getAdAssetById = await Mediator.Send(new GetAdAssetByIdQuery(request.AdAssetId), cancellationToken);
            if (getAdAssetById.Ok)
            {
                await Mediator.Send(new UpdateAdCacheCommand(getAdAssetById.Data.AdId));
            }

            return Unit.Value;
        }
    }
}
