using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Queries.IsExistsAdAssetStatsById
{
    public class IsExistsAdAssetStatsByIdQueryHandler : IRequestHandler<IsExistsAdAssetStatsByIdQuery, HttpResult<bool>>
    {
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public IsExistsAdAssetStatsByIdQueryHandler(AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdAssetStatsByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdStatsDbRepo.AnyAsync(f =>
                f.AdId == request.Id
            );

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdStats Id='{request.Id}' doesn't exist.");
        }
    }
}
