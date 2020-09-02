using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Analytics.AdGroupStatsList.Queries.IsExistsAdGroupStatsById
{
    public class IsExistsAdGroupStatsByIdQueryHandler : IRequestHandler<IsExistsAdGroupStatsByIdQuery, HttpResult<bool>>
    {
        private AdsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo { get; }

        public IsExistsAdGroupStatsByIdQueryHandler(AdsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdGroupStatsByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdGroupStatsDbRepo.AnyAsync(f =>
                f.Id == request.Id
            );

            var result = new HttpResult<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdGroupStats '{request.Id}' doesn't exist.");
        }
    }
}
