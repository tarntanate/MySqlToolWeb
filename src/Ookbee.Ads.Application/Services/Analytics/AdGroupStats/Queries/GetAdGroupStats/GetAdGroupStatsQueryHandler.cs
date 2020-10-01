using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStats
{
    public class GetAdGroupStatsQueryHandler : IRequestHandler<GetAdGroupStatsQuery, Response<AdGroupStatsDto>>
    {
        private readonly AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo;

        public GetAdGroupStatsQueryHandler(
            AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Response<AdGroupStatsDto>> Handle(GetAdGroupStatsQuery request, CancellationToken cancellationToken)
        {
            var item = await AdGroupStatsDbRepo.FirstAsync(
                selector: AdGroupStatsDto.Projection,
                filter: f =>
                    f.AdGroupId == request.AdGroupId &&
                    f.CaculatedAt == request.CaculatedAt
            );

            var result = new Response<AdGroupStatsDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
