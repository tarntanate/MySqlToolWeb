using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.UpdateAdAssetCache;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.UpdateAdAssetCacheByAssetId
{
    public class UpdateAdAssetCacheByAssetIdCommandHandler : IRequestHandler<UpdateAdAssetCacheByAssetIdCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdAssetCacheByAssetIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
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
