using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.DeleteAdAssetCache;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.DeleteAdAsset
{
    public class DeleteAdAssetCommandHandler : IRequestHandler<DeleteAdAssetCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public DeleteAdAssetCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdAssetCommand request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<bool>();
            var adAssetResult = await Mediator.Send(new GetAdAssetByIdQuery(request.Id), cancellationToken);

            if (!adAssetResult.Ok)
                return result.Fail(adAssetResult.StatusCode, adAssetResult.Message);

            await Mediator.Send(new DeleteAdAssetCacheCommand(adAssetResult.Data.AdId), cancellationToken);
            await AdAssetDbRepo.DeleteAsync(request.Id);
            await AdAssetDbRepo.SaveChangesAsync(cancellationToken);

            return result.Success(true);
        }
    }
}
