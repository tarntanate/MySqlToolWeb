using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.UpdateAdAssetCache;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.CeateAdAssetCacheByAssetId
{
    public class CreateAdAssetCacheByAssetIdCommandHandler : IRequestHandler<CreateAdAssetCacheByAssetIdCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdAssetCacheByAssetIdCommandHandler(
            IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task<Unit> Handle(CreateAdAssetCacheByAssetIdCommand request, CancellationToken cancellationToken)
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
