using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.GetAdUnitTypeById
{
    public class GetAdUnitTypeByIdQueryHandler : IRequestHandler<GetAdUnitTypeByIdQuery, Response<AdUnitTypeDto>>
    {
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public GetAdUnitTypeByIdQueryHandler(AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<Response<AdUnitTypeDto>> Handle(GetAdUnitTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await AdUnitTypeDbRepo.FirstAsync(
                selector: AdUnitTypeDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new Response<AdUnitTypeDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdUnitType '{request.Id}' doesn't exist.");
        }
    }
}
