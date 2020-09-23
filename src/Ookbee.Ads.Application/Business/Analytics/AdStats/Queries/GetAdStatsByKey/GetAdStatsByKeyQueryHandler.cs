using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdStatsByKey
{
    public class GetAdStatsByKeyQueryHandler : IRequestHandler<GetAdStatsByKeyQuery, Response<AdStatsDto>>
    {
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public GetAdStatsByKeyQueryHandler(AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Response<AdStatsDto>> Handle(GetAdStatsByKeyQuery request, CancellationToken cancellationToken)
        {
            var item = await AdStatsDbRepo.FirstAsync(
                selector: AdStatsDto.Projection,
                filter: f =>
                    f.AdId == request.AdId &&
                    f.CaculatedAt == request.CaculatedAt
            );

            var result = new Response<AdStatsDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Ad Stat Key not found.");
        }
    }
}
