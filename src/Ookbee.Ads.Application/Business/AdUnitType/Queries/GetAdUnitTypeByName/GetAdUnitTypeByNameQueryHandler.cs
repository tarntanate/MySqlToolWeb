using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeByName
{
    public class GetAdUnitTypeByNameQueryHandler : IRequestHandler<GetAdUnitTypeByNameQuery, HttpResult<AdUnitTypeDto>>
    {
        private AdsEFCoreRepository<AdUnitTypeEntity> AdUnitTypeEFCoreRepo { get; }

        public GetAdUnitTypeByNameQueryHandler(AdsEFCoreRepository<AdUnitTypeEntity> adUnitTypeEFCoreRepo )
        {
            AdUnitTypeEFCoreRepo = adUnitTypeEFCoreRepo;
        }

        public async Task<HttpResult<AdUnitTypeDto>> Handle(GetAdUnitTypeByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdUnitTypeDto>> GetOnDb(GetAdUnitTypeByNameQuery request)
        {
            var result = new HttpResult<AdUnitTypeDto>();

            var item = await AdUnitTypeEFCoreRepo.FirstAsync(filter: f => f.Name == request.Name && f.DeletedAt == null);
            if (item == null)
                return result.Fail(404, $"AdUnitType '{request.Name}' doesn't exist.");

            var data = Mapper
                .Map(item)
                .ToANew<AdUnitTypeDto>();
            
            return result.Success(data);
        }
    }
}
