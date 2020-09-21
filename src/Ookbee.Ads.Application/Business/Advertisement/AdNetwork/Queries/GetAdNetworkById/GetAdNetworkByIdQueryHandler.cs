using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Queries.GetAdNetworkById
{
    public class GetAdNetworkByIdQueryHandler : IRequestHandler<GetAdNetworkByIdQuery, HttpResult<AdNetworkDto>>
    {
        private AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo { get; }

        public GetAdNetworkByIdQueryHandler(AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<HttpResult<AdNetworkDto>> Handle(GetAdNetworkByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdNetworkDbRepo.FirstAsync(
                selector: AdNetworkDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new HttpResult<AdNetworkDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdNetwork '{request.Id}' doesn't exist.");
        }
    }
}
