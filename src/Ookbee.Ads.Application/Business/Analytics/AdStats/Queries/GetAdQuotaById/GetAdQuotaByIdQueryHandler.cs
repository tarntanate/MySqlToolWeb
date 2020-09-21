using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Domain.Entities.AnalyticsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using Ookbee.Ads.Persistence.EFCore.AnalyticsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdQuotaById
{
    public class GetAdQuotaByIdQueryHandler : IRequestHandler<GetAdQuotaByIdQuery, HttpResult<int>>
    {
        private AdsDbRepository<AdEntity> AdDbRepo { get; }
        private AnalyticsDbRepository<AdStatsEntity> AdStatsDbRepo { get; }

        public GetAdQuotaByIdQueryHandler(
            AdsDbRepository<AdEntity> adDbRepo,
            AnalyticsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdDbRepo = adDbRepo;
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<HttpResult<int>> Handle(GetAdQuotaByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new HttpResult<int>();

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
                return result.Success(avalible);
            }
            return result.Fail(404, "Data not found.");
        }
    }
}
