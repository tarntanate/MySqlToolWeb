using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdAssetCache.Commands.DeleteAdAssetCacheByAssetId;
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

            await Mediator.Send(new DeleteAdAssetCacheByAssetIdCommand(request.Id), cancellationToken);
            await AdAssetDbRepo.DeleteAsync(request.Id);
            await AdAssetDbRepo.SaveChangesAsync(cancellationToken);

            return result.Success(true);
        }
    }
}
