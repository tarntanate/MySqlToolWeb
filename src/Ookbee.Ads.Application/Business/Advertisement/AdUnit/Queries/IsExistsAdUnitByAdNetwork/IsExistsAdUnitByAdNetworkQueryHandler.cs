using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdNetwork
{
    public class IsExistsAdUnitByAdNetworkQueryHandler : IRequestHandler<IsExistsAdUnitByAdNetworkQuery, Response<bool>>
    {
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public IsExistsAdUnitByAdNetworkQueryHandler(AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdUnitByAdNetworkQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdUnitDbRepo.AnyAsync(f =>
                f.AdNetwork == request.AdNetwork &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.Success(true)
                : result.Fail(404, $"AdUnit By AdNetwork '{request.AdNetwork}' doesn't exist.");
        }
    }
}
