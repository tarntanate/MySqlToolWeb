using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdStatsByKey
{
    public class GetAdStatsByKeyQueryHandler : IRequestHandler<GetAdStatsByKeyQuery, HttpResult<AdStatsDto>>
    {
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public GetAdStatsByKeyQueryHandler(AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<HttpResult<AdStatsDto>> Handle(GetAdStatsByKeyQuery request, CancellationToken cancellationToken)
        {
            var item = await AdStatsDbRepo.FirstAsync(
                selector: AdStatsDto.Projection,
                filter: f =>
                    f.AdId == request.AdId &&
                    f.Platform == request.Platform &&
                    f.CaculatedAt == request.CaculatedAt
            );

            var result = new HttpResult<AdStatsDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdStats by AdId='{request.AdId}' and Platform='{request.Platform.ToString()}' and CaculatedAt='{request.CaculatedAt}' doesn't exist.");
        }
    }
}
