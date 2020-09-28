using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Queries.GetAdUnitById
{
    public class GetAdUnitByIdQueryHandler : IRequestHandler<GetAdUnitByIdQuery, Response<AdUnitDto>>
    {
        private readonly AdsDbRepository<AdUnitEntity> AdUnitDbRepo;

        public GetAdUnitByIdQueryHandler(
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<Response<AdUnitDto>> Handle(GetAdUnitByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdUnitDbRepo.FirstAsync(
                selector: AdUnitDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new Response<AdUnitDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
