using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdGroup
{
    public class IsExistsAdUnitByAdGroupQueryHandler : IRequestHandler<IsExistsAdUnitByAdGroupQuery, Response<bool>>
    {
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public IsExistsAdUnitByAdGroupQueryHandler(
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdUnitByAdGroupQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdUnitDbRepo.AnyAsync(f =>
                f.AdNetwork == request.AdNetworkName &&
                f.AdGroupId == request.AdGroupId &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
