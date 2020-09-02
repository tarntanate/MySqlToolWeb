using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStatsList.Queries.IsExistsAdStatsById
{
    public class IsExistsAdStatsByIdQueryHandler : IRequestHandler<IsExistsAdStatsByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public IsExistsAdStatsByIdQueryHandler(AdsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdStatsByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdStatsDbRepo.AnyAsync(f =>
                f.Id == request.Id
            );

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdStats '{request.Id}' doesn't exist.");
        }
    }
}
