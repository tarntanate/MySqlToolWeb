using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.AdAsset.Queries.IsExistsAdAssetById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.DeleteAdAsset
{
    public class DeleteAdAssetCommandHandler : IRequestHandler<DeleteAdAssetCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdAssetEntity> AdAssetEFCoreRepo { get; }

        public DeleteAdAssetCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdAssetEntity> adUnitEFCoreRepo)
        {
            Mediator = mediator;
            AdAssetEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdAssetCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteOnDb(DeleteAdAssetCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsResult = await Mediator.Send(new IsExistsAdAssetByIdQuery(request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await AdAssetEFCoreRepo.DeleteAsync(request.Id);
            await AdAssetEFCoreRepo.SaveChangesAsync();
            
            return result.Success(true);
        }
    }
}
