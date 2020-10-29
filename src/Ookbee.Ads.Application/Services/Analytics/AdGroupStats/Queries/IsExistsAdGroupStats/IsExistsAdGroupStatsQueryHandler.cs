using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.IsExistsAdGroupStats
{
    public class IsExistsAdGroupStatsQueryHandler : IRequestHandler<IsExistsAdGroupStatsQuery, Response<bool>>
    {
        private readonly AdsDbRepository<AdGroupStatsEntity> AdGroupStatsDbRepo;

        public IsExistsAdGroupStatsQueryHandler(
            AdsDbRepository<AdGroupStatsEntity> adGroupStatsDbRepo)
        {
            AdGroupStatsDbRepo = adGroupStatsDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdGroupStatsQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdGroupStatsDbRepo.AnyAsync(f =>
                f.AdGroupId == request.AdGroupId &&
                f.CaculatedAt == request.CaculatedAt
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
