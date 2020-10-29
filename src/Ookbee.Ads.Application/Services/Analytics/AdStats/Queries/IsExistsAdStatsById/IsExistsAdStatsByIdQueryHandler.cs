using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.IsExistsAdStatsById
{
    public class IsExistsAdStatsByIdQueryHandler : IRequestHandler<IsExistsAdStatsByIdQuery, Response<bool>>
    {
        private readonly AdsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public IsExistsAdStatsByIdQueryHandler(AdsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdStatsByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdStatsDbRepo.AnyAsync(f =>
                f.AdId == request.Id
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
