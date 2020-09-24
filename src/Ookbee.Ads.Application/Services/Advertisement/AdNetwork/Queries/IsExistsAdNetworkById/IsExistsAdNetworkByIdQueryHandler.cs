using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Queries.IsExistsAdNetworkById
{
    public class IsExistsAdNetworkByIdQueryHandler : IRequestHandler<IsExistsAdNetworkByIdQuery, Response<bool>>
    {
        private readonly AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo;

        public IsExistsAdNetworkByIdQueryHandler(
            AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsAdNetworkByIdQuery request, CancellationToken cancellationToken)
        {
            var isExists = await AdNetworkDbRepo.AnyAsync(f =>
                f.Id == request.Id &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
