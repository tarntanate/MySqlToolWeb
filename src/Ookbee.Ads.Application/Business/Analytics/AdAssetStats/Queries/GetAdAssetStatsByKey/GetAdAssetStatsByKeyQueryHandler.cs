using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Queries.GetAdAssetStatsByKey
{
    public class GetAdAssetStatsByKeyQueryHandler : IRequestHandler<GetAdStatsByKeyQuery, HttpResult<AdAssetStatsDto>>
    {
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public GetAdAssetStatsByKeyQueryHandler(AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<HttpResult<AdAssetStatsDto>> Handle(GetAdStatsByKeyQuery request, CancellationToken cancellationToken)
        {
            var item = await AdStatsDbRepo.FirstAsync(
                selector: AdAssetStatsDto.Projection,
                filter: f =>
                    f.AdId == request.AdId &&
                    f.Platform == request.Platform &&
                    f.CaculatedAt == request.CaculatedAt
            );

            var result = new HttpResult<AdAssetStatsDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdStats by AdId='{request.AdId}' and Platform='{request.Platform.ToString()}' and CaculatedAt='{request.CaculatedAt}' doesn't exist.");
        }
    }
}
