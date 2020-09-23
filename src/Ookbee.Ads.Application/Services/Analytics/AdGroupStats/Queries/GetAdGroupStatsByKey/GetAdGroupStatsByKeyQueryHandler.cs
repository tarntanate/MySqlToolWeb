using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsByKey
{
    public class GetAdGroupStatsByKeyQueryHandler : IRequestHandler<GetAdGroupStatsByKeyQuery, Response<AdGroupStatsDto>>
    {
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public GetAdGroupStatsByKeyQueryHandler(AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Response<AdGroupStatsDto>> Handle(GetAdGroupStatsByKeyQuery request, CancellationToken cancellationToken)
        {
            var item = await AdGroupStatsDbRepo.FirstAsync(
                selector: AdGroupStatsDto.Projection,
                filter: f =>
                    f.AdGroupId == request.AdGroupId &&
                    f.CaculatedAt == request.CaculatedAt
            );

            var result = new Response<AdGroupStatsDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Data not found.");
        }
    }
}
