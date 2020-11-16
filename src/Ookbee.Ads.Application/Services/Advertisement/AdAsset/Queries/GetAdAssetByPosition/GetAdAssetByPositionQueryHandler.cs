using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetByPosition
{
    public class GetAdAssetByPositionQueryHandler : IRequestHandler<GetAdAssetByPositionQuery, Response<AdAssetDto>>
    {
        private readonly AdsDbRepository<AdAssetEntity> AdAssetDbRepo;

        public GetAdAssetByPositionQueryHandler(
            AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<Response<AdAssetDto>> Handle(GetAdAssetByPositionQuery request, CancellationToken cancellationToken)
        {
            var item = await AdAssetDbRepo.FirstAsync<AdAssetDto>(
                filter: f =>
                    f.AdId == request.AdId &&
                    f.Position == request.Position &&
                    f.DeletedAt == null
            );

            var result = new Response<AdAssetDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
