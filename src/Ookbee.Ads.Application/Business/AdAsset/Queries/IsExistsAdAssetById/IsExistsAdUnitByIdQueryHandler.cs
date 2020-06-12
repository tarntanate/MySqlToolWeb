using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.IsExistsAdAssetById
{
    public class IsExistsAdAssetByIdQueryHandler : IRequestHandler<IsExistsAdAssetByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public IsExistsAdAssetByIdQueryHandler(AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdAssetByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsOnDb(request);
        }

        private async Task<HttpResult<bool>> IsExistsOnDb(IsExistsAdAssetByIdQuery request)
        {
            var result = new HttpResult<bool>();

            var isExists = await AdAssetDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            if (!isExists)
                return result.Fail(404, $"AdAsset '{request.Id}' doesn't exist.");
            return result.Success(true);
        }
    }
}
