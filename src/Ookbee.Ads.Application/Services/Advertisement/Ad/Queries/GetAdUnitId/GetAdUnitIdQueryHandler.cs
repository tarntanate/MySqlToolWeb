using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.GetAdUnitId
{
    public class GetAdUnitIdQueryHandler : IRequestHandler<GetAdUnitIdQuery, Response<long>>
    {
        private readonly AdsDbRepository<AdEntity> AdDbRepo;

        public GetAdUnitIdQueryHandler(
            AdsDbRepository<AdEntity> adDbRepo)
        {
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<long>> Handle(GetAdUnitIdQuery request, CancellationToken cancellationToken)
        {
            var adUnitId = await AdDbRepo.FirstAsync(
                selector: f => f.AdUnitId,
                filter: f => 
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new Response<long>();
            return (adUnitId > 0)
                ? result.OK(adUnitId)
                : result.NotFound();
        }
    }
}
