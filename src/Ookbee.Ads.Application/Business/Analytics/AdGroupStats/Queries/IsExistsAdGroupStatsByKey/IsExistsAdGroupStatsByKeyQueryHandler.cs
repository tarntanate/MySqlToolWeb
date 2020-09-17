using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.IsExistsAdGroupStatsByKey
{
    public class IsExistsAdGroupStatsByKeyQueryHandler : IRequestHandler<IsExistsAdGroupStatsByKeyQuery, HttpResult<bool>>
    {
        private AnalyticsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public IsExistsAdGroupStatsByKeyQueryHandler(AnalyticsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdGroupStatsByKeyQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdGroupStatsDbRepo.AnyAsync(f =>
                f.AdGroupId == request.AdGroupId &&
                f.CaculatedAt == request.CaculatedAt
            );

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdGroupStats by GroupId='{request.AdGroupId}' and CaculatedAt='{request.CaculatedAt}' doesn't exist.");
        }
    }
}
