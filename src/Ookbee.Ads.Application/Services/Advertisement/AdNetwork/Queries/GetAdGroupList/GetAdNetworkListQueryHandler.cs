using MediatR;
using Ookbee.Ads.Common.Builders;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkList
{
    public class GetAdNetworkListQueryHandler : IRequestHandler<GetAdNetworkListQuery, Response<IEnumerable<AdNetworkDto>>>
    {
        private AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo { get; }

        public GetAdNetworkListQueryHandler(
            AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<Response<IEnumerable<AdNetworkDto>>> Handle(GetAdNetworkListQuery request, CancellationToken cancellationToken)
        {
            var predicate = PredicateBuilder.True<AdNetworkEntity>();
            predicate = predicate.And(f => f.DeletedAt == null);

            if (request.AdUnitId.HasValue())
                predicate = predicate.And(f => f.AdUnitId == request.AdUnitId);

            var items = await AdNetworkDbRepo.FindAsync(
                selector: AdNetworkDto.Projection,
                filter: predicate,
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<AdNetworkDto>>();
            return (items.HasValue())
                ? result.Success(items)
                : result.Fail(404, $"Data not found.");
        }
    }
}
