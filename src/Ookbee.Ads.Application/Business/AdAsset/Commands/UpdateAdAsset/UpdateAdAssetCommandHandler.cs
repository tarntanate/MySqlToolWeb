using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetById;
using Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetByPosition;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommandHandler : IRequestHandler<UpdateAdAssetCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdAssetEntity> AdAssetEFCoreRepo { get; }

        public UpdateAdAssetCommandHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdAssetEntity> adUnitEFCoreRepo)
        {
            Mediator = mediator;
            AdAssetEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdAssetCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateAdAssetCommand request)
        {
            var result = new HttpResult<bool>();
            
            var adAssetResult = await Mediator.Send(new GetAdAssetByIdQuery(request.Id));
            if (!adAssetResult.Ok)
                return result.Fail(adAssetResult.StatusCode, adAssetResult.Message);

            var adAssetByPositionResult = await Mediator.Send(new GetAdAssetByPositionQuery(request.Position));
            if (adAssetByPositionResult.Ok &&
                adAssetByPositionResult.Data.Id != request.Id)
                return result.Fail(409, $"AdAsset '{request.Position.ToString()}' already exists.");

            var source = Mapper
                .Map(request)
                .Over(adAssetResult.Data);
            var entity = Mapper
                .Map(source)
                .ToANew<AdAssetEntity>();

            await AdAssetEFCoreRepo.UpdateAsync(entity.Id, entity);
            await AdAssetEFCoreRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
