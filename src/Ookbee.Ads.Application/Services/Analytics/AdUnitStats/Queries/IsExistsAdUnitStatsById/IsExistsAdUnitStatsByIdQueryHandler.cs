using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStats.Queries.IsExistsAdUnitStatsById
{
    public class IsExistsAdUnitStatsByIdQueryHandler : IRequestHandler<IsExistsAdUnitStatsByIdQuery, Response<bool>>
    {
        private AdsDbRepository<AdUnitStatsEntity> AdUnitStatsDbRepo { get; }

        public IsExistsAdUnitStatsByIdQueryHandler(AdsDbRepository<AdUnitStatsEntity> adUnitStatsDbRepo)
        {
            AdUnitStatsDbRepo = adUnitStatsDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdUnitStatsByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdUnitStatsDbRepo.AnyAsync(f =>
                f.Id == request.Id
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdUnitStats '{request.Id}' doesn't exist.");
        }
    }
}
