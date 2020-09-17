using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.IsExistsAStatsByKey
{
    public class IsExistsAdStatsByKeyQueryHandler : IRequestHandler<IsExistsAdStatsByKeyQuery, HttpResult<bool>>
    {
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public IsExistsAdStatsByKeyQueryHandler(AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdStatsByKeyQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdStatsDbRepo.AnyAsync(f =>
                f.AdId == request.AdId &&
                f.CaculatedAt == request.CaculatedAt
            );

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdStats by AdId='{request.AdId}' and CaculatedAt='{request.CaculatedAt}' doesn't exist.");
        }
    }
}
