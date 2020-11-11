using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkByPlatform
{
    public class GetAdNetworkListByGroupIdQueryHandler : IRequestHandler<GetAdNetworkByPlatformQuery, Response<AdNetworkDto>>
    {
        private readonly AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo;

        public GetAdNetworkListByGroupIdQueryHandler(
            AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<Response<AdNetworkDto>> Handle(GetAdNetworkByPlatformQuery request, CancellationToken cancellationToken)
        {
            var item = await AdNetworkDbRepo.FirstAsync<AdNetworkDto>(
                filter: f =>
                    f.AdUnitId == request.AdUnitId &&
                    f.Platform == request.Platform &&
                    f.DeletedAt == null
            );

            var result = new Response<AdNetworkDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
