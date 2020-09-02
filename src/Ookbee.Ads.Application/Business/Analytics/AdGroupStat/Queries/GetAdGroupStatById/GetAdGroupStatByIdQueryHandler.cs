using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.GetAdGroupStatById
{
    public class GetAdGroupStatByIdQueryHandler : IRequestHandler<GetAdGroupStatByIdQuery, HttpResult<AdGroupStatsDto>>
    {
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public GetAdGroupStatByIdQueryHandler(AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<HttpResult<AdGroupStatsDto>> Handle(GetAdGroupStatByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdGroupStatsDbRepo.FirstAsync(
                selector: AdGroupStatsDto.Projection,
                filter: f =>
                    f.AdGroupId == request.AdGroupId &&
                    f.CaculatedAt == request.CaculatedAt
            );

            var result = new HttpResult<AdGroupStatsDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdGroupStats by GroupId='{request.AdGroupId}' and CaculatedAt='{request.CaculatedAt}' doesn't exist.");
        }
    }
}
