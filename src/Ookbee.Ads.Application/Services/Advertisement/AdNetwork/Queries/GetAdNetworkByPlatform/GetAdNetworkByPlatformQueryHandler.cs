using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.GetAdNetworkByPlatform
{
    public class GetAdNetworkByPlatformQueryHandler : IRequestHandler<GetAdNetworkByPlatformQuery, Response<AdNetworkDto>>
    {
        private AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo { get; }

        public GetAdNetworkByPlatformQueryHandler(AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<Response<AdNetworkDto>> Handle(GetAdNetworkByPlatformQuery request, CancellationToken cancellationToken)
        {
            var item = await AdNetworkDbRepo.FirstAsync(
                selector: AdNetworkDto.Projection,
                filter: f =>
                    f.Platform == request.Platform &&
                    f.DeletedAt == null);

            var result = new Response<AdNetworkDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdNetwork '{request.Platform}' doesn't exist.");
        }
    }
}
