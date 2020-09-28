using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdQuotaById
{
    public class GetAdQuotaByIdQueryHandler : IRequestHandler<GetAdQuotaByIdQuery, Response<int>>
    {
        private readonly AdsDbRepository<AdEntity> AdDbRepo;
        private readonly AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public GetAdQuotaByIdQueryHandler(
            AdsDbRepository<AdEntity> adDbRepo,
            AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdDbRepo = adDbRepo;
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Response<int>> Handle(GetAdQuotaByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new Response<int>();

            var ad = await AdDbRepo.FirstAsync(
                filter: f => f.Id == request.AdId
            );

            if (ad.HasValue())
            {
                var impressions = await AdStatsDbRepo.SumAsync(
                    filter: f =>
                        f.AdId == request.AdId &&
                        f.CaculatedAt < request.CaculatedAt,
                    selector: f =>
                        f.Impression
                );
                var avalible = ad.Quota - (int)impressions;
                avalible = avalible < 0 ? 0 : avalible;
                return result.OK(avalible);
            }
            return result.NotFound();
        }
    }
}
