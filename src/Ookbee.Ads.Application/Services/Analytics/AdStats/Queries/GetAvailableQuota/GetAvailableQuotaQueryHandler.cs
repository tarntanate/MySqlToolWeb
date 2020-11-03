using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAvailableQuota
{
    public class GetAvailableQuotaHandler : IRequestHandler<GetAvailableQuotaQuery, Response<int>>
    {
        private readonly AdsDbRepository<AdEntity> AdDbRepo;
        private readonly AdsDbRepository<AdStatsEntity> AdStatsDbRepo;

        public GetAvailableQuotaHandler(
            AdsDbRepository<AdEntity> adDbRepo,
            AdsDbRepository<AdStatsEntity> adStatsDbRepo)
        {
            AdDbRepo = adDbRepo;
            AdStatsDbRepo = adStatsDbRepo;
        }

        public async Task<Response<int>> Handle(GetAvailableQuotaQuery request, CancellationToken cancellationToken)
        {
            var result = new Response<int>();

            var ad = await AdDbRepo.FirstAsync(
                filter: f => 
                    f.Id == request.AdId &&
                    f.StartAt <= request.CaculatedAt &&
                    f.EndAt >= request.CaculatedAt
            );

            if (ad.HasValue())
            {
                var totalImpression = await AdStatsDbRepo.SumAsync(
                    filter: f =>
                        f.AdId == request.AdId &&
                        f.CaculatedAt < request.CaculatedAt,
                    selector: f =>
                        f.Impression
                );
                var days = (int)Math.Ceiling((ad.EndAt - ad.StartAt).TotalDays) + 1;
                var daysLeft = (int)Math.Ceiling((ad.EndAt - MechineDateTime.Date).TotalDays) + 1;
                var remainingQuota = ad.Quota - totalImpression;
                var todayQuota = (int)Math.Ceiling(remainingQuota / daysLeft);
                return result.OK(todayQuota);
            }
            return result.NotFound();
        }
    }
}
