using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.GetAdUnitTypeByName
{
    public class GetAdUnitTypeByNameQueryHandler : IRequestHandler<GetAdUnitTypeByNameQuery, HttpResult<AdUnitTypeDto>>
    {
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public GetAdUnitTypeByNameQueryHandler(AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<HttpResult<AdUnitTypeDto>> Handle(GetAdUnitTypeByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await AdUnitTypeDbRepo.FirstAsync(
                selector: AdUnitTypeDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null);

            var result = new HttpResult<AdUnitTypeDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"AdUnitType '{request.Name}' doesn't exist.");
        }
    }
}
