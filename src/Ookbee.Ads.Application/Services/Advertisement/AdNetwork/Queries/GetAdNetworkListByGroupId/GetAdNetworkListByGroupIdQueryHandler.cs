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

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkListByGroupId
{
    public class GetAdNetworkListByGroupIdQueryHandler : IRequestHandler<GetAdNetworkListByGroupIdQuery, Response<IEnumerable<AdNetworkDto>>>
    {
        private readonly AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo;

        public GetAdNetworkListByGroupIdQueryHandler(
            AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<Response<IEnumerable<AdNetworkDto>>>  Handle(GetAdNetworkListByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdNetworkEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdGroupId.HasValue())
                predicate = predicate.And(f => f.AdUnit.AdGroupId == request.AdGroupId);

            var items = await AdNetworkDbRepo.FindAsync<AdNetworkDto>(
                filter: predicate,
                orderBy: f => f.OrderBy(o => o.AdUnit.AdNetwork),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<AdNetworkDto>>();
            return (items.HasValue())
                ? result.OK(items)
                : result.NotFound();
        }
    }
}
