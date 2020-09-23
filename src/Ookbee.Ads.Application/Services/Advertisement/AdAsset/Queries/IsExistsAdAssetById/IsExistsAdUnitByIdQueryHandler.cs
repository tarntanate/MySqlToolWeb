using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.IsExistsAdAssetById
{
    public class IsExistsAdAssetByIdQueryHandler : IRequestHandler<IsExistsAdAssetByIdQuery, Response<bool>>
    {
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public IsExistsAdAssetByIdQueryHandler(AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdAssetByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdAssetDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdAsset '{request.Id}' doesn't exist.");
        }
    }
}
