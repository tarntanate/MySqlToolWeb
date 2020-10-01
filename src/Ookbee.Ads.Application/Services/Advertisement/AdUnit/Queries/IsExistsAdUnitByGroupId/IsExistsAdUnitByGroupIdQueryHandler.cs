using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.IsExistsAdUnitByAdGroupId
{
    public class IsExistsAdUnitByGroupIdQueryHandler : IRequestHandler<IsExistsAdUnitByGroupIdQuery, Response<bool>>
    {
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public IsExistsAdUnitByGroupIdQueryHandler(
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdUnitByGroupIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdUnitDbRepo.AnyAsync(f =>
                f.AdGroupId == request.AdGroupId &&
                f.AdNetwork == request.AdNetworkName &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
