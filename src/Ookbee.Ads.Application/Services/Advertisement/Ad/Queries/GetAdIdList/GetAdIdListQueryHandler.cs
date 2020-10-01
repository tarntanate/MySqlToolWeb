using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdIdList
{
    public class GetAdIdListQueryHandler : IRequestHandler<GetAdIdListQuery, Response<IEnumerable<long>>>
    {
        private readonly AdsDbRepository<AdEntity> AdDbRepo;

        public GetAdIdListQueryHandler(
            AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<IEnumerable<long>>> Handle(GetAdIdListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdUnitId.HasValue() && request.AdUnitId > 0)
                predicate = predicate.And(f => f.AdUnitId == request.AdUnitId);

            if (request.CampaignId.HasValue() && request.CampaignId > 0)
                predicate = predicate.And(f => f.CampaignId == request.CampaignId);

            var items = await AdDbRepo.FindAsync(
                selector: f => new { f.Id },
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<long>>();
            return (items.HasValue())
                ? result.OK(items.Select(v => v.Id).ToList())
                : result.NotFound();
        }
    }
}
