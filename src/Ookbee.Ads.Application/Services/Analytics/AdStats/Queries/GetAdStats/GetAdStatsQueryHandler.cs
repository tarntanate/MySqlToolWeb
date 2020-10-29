using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdStats
{
    public class GetAdStatsQueryHandler : IRequestHandler<GetAdStatsQuery, Response<AdStatsDto>>
    {
        private readonly AdsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public GetAdStatsQueryHandler(
            AdsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Response<AdStatsDto>> Handle(GetAdStatsQuery request, CancellationToken cancellationToken)
        {
            var item = await AdStatsDbRepo.FirstAsync(
                selector: AdStatsDto.Projection,
                filter: f =>
                    f.AdId == request.AdId &&
                    f.CaculatedAt == request.CaculatedAt
            );

            var result = new Response<AdStatsDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
