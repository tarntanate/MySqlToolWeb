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

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitIdList
{
    public class GetAdUnitIdListQueryHandler : IRequestHandler<GetAdUnitIdListQuery, Response<IEnumerable<long>>>
    {
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public GetAdUnitIdListQueryHandler(
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<IEnumerable<long>>> Handle(GetAdUnitIdListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdUnitEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdGroupId.HasValue())
                predicate = predicate.And(f => f.AdGroupId == request.AdGroupId);

            var items = await AdUnitDbRepo.FindAsync(
                selector: f => new { f.Id },
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.SortSeq),
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
