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

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupIdList
{
    public class GetAdGroupIdListQueryHandler : IRequestHandler<GetAdGroupIdListQuery, Response<IEnumerable<long>>>
    {
        private readonly AdsDbRepository<AdGroupEntity> AdGroupDbRepo;

        public GetAdGroupIdListQueryHandler(
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Response<IEnumerable<long>>> Handle(GetAdGroupIdListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdGroupEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.Enabled.HasValue())
                predicate = predicate.And(f => f.Enabled == request.Enabled);
                
            if (request.AdUnitTypeId.HasValue())
                predicate = predicate.And(f => f.AdUnitTypeId == request.AdUnitTypeId);

            if (request.PublisherId.HasValue())
                predicate = predicate.And(f => f.PublisherId == request.PublisherId);

            var items = await AdGroupDbRepo.FindAsync(
                selector: f => new { Id = f.Id },
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
