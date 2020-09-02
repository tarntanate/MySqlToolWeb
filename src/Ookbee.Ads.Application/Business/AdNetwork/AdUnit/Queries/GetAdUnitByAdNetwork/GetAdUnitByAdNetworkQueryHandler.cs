using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnit.Queries.GetAdUnitByAdNetwork
{
    public class GetAdUnitByAdNetworkQueryHandler : IRequestHandler<GetAdUnitByAdNetworkQuery, HttpResult<AdUnitDto>>
    {
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public GetAdUnitByAdNetworkQueryHandler(AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<AdUnitDto>> Handle(GetAdUnitByAdNetworkQuery request, CancellationToken cancellationToken)
        {
            var item = await AdUnitDbRepo.FirstAsync(
                selector: AdUnitDto.Projection,
                filter: f =>
                    f.AdNetwork == request.AdNetwork &&
                    f.DeletedAt == null
            );

            var result = new HttpResult<AdUnitDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdUnit By AdNetwork '{request.AdNetwork}' doesn't exist.");
        }
    }
}
