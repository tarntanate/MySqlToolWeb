using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.GetAdNetworkByPlatform
{
    public class GetAdNetworkByPlatformQueryHandler : IRequestHandler<GetAdNetworkByPlatformQuery, HttpResult<AdNetworkDto>>
    {
        private AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo { get; }

        public GetAdNetworkByPlatformQueryHandler(AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<HttpResult<AdNetworkDto>> Handle(GetAdNetworkByPlatformQuery request, CancellationToken cancellationToken)
        {
            var item = await AdNetworkDbRepo.FirstAsync(
                selector: AdNetworkDto.Projection,
                filter: f =>
                    f.Platform == request.Platform &&
                    f.DeletedAt == null);

            var result = new HttpResult<AdNetworkDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdNetwork '{request.Platform}' doesn't exist.");
        }
    }
}
