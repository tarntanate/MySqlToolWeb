using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetByPosition
{
    public class GetAdAssetByPositionQueryHandler : IRequestHandler<GetAdAssetByPositionQuery, HttpResult<AdAssetDto>>
    {
        private AdsDbRepository<AdAssetEntity> AdAssetDbRepo { get; }

        public GetAdAssetByPositionQueryHandler(AdsDbRepository<AdAssetEntity> adUnitDbRepo)
        {
            AdAssetDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<AdAssetDto>> Handle(GetAdAssetByPositionQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdAssetDto>> GetOnDb(GetAdAssetByPositionQuery request)
        {
            var result = new HttpResult<AdAssetDto>();

            var item = await AdAssetDbRepo.FirstAsync(filter: f => f.Id == request.Id && f.Position == request.Position);
            if (item == null)
                return result.Fail(404, $"AdAsset '{request.Position.ToString()}' doesn't exist.");

            var data = Mapper
                .Map(item)
                .ToANew<AdAssetDto>();

            return result.Success(data);
        }
    }
}
