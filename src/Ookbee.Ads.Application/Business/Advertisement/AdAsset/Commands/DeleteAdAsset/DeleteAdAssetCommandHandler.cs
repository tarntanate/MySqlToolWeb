using MediatR;
using Ookbee.Ads.Application.Business.Cache.AdCache.Commands.DeleteAdCacheByAssetId;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.DeleteAdAsset
{
    public class DeleteAdAssetCommandHandler : IRequestHandler<DeleteAdAssetCommand, Response<bool>>
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

        public async Task<Response<bool>> Handle(DeleteAdAssetCommand request, CancellationToken cancellationToken)
        {
            var result = new Response<bool>();

            await Mediator.Send(new DeleteAdCacheByAssetIdCommand(request.Id), cancellationToken);
            await AdAssetDbRepo.DeleteAsync(request.Id);
            await AdAssetDbRepo.SaveChangesAsync(cancellationToken);

            return result.Success(true);
        }
    }
}
