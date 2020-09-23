using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.IsExistsAdAssetByPosition
{
    public class IsExistsAdAssetByPositionQueryHandler : IRequestHandler<IsExistsAdAssetByPositionQuery, Response<bool>>
    {
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public IsExistsAdAssetByPositionQueryHandler(AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdAssetByPositionQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdAssetDbRepo.AnyAsync(f =>
                f.AdId == request.AdId &&
                f.Position == request.Position &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdAsset '{request.Position.ToString()}' doesn't exist.");
        }
    }
}
