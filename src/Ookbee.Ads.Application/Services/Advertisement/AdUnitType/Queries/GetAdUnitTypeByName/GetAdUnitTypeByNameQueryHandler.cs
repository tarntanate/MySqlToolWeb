using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.GetAdUnitTypeByName
{
    public class GetAdUnitTypeByNameQueryHandler : IRequestHandler<GetAdUnitTypeByNameQuery, Response<AdUnitTypeDto>>
    {
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public GetAdUnitTypeByNameQueryHandler(AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<Response<AdUnitTypeDto>> Handle(GetAdUnitTypeByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await AdUnitTypeDbRepo.FirstAsync(
                selector: AdUnitTypeDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null);

            var result = new Response<AdUnitTypeDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdUnitType '{request.Name}' doesn't exist.");
        }
    }
}
