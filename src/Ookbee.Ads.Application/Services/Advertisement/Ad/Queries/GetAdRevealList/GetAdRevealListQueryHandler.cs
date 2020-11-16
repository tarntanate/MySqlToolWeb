using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdRevealList
{
    public class GetAdRevealListQueryHandler : IRequestHandler<GetAdRevealListQuery, Response<IEnumerable<AdDto>>>
    {
        private readonly AdsDbRepository<AdEntity> AdDbRepo;

        public GetAdRevealListQueryHandler(
            AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<IEnumerable<AdDto>>> Handle(GetAdRevealListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);
            predicate = predicate.And(f => f.Status == AdStatusType.Preview || f.Status == AdStatusType.Publish);
            predicate = predicate.And(f => f.StartAt <= MechineDateTime.Now && f.EndAt >= MechineDateTime.Now);

            if (request.AdUnitId.HasValue())
                predicate = predicate.And(f => f.AdUnitId == request.AdUnitId);

            if (request.CampaignId.HasValue())
                predicate = predicate.And(f => f.CampaignId == request.CampaignId);

            var items = await AdDbRepo.FindAsync<AdDto>(
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<AdDto>>();
            return (items.HasValue())
                ? result.OK(items)
                : result.NotFound();
        }
    }
}
