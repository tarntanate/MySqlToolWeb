using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.GetAdUnitByAdNetwork
{
    public class GetAdUnitByAdNetworkQueryHandler : IRequestHandler<GetAdUnitByAdNetworkQuery, Response<AdUnitDto>>
    {
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public GetAdUnitByAdNetworkQueryHandler(AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<AdUnitDto>> Handle(GetAdUnitByAdNetworkQuery request, CancellationToken cancellationToken)
        {
            var item = await AdUnitDbRepo.FirstAsync(
                selector: AdUnitDto.Projection,
                filter: f =>
                    f.AdNetwork == request.AdNetwork &&
                    f.DeletedAt == null
            );

            var result = new Response<AdUnitDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdUnit By AdNetwork '{request.AdNetwork}' doesn't exist.");
        }
    }
}
