using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeById
{
    public class GetAdUnitTypeByIdQueryHandler : IRequestHandler<GetAdUnitTypeByIdQuery, HttpResult<AdUnitTypeDto>>
    {
        private AdsEFCoreRepository<AdUnitTypeEntity> AdUnitTypeEFCoreRepo { get; }

        public GetAdUnitTypeByIdQueryHandler(AdsEFCoreRepository<AdUnitTypeEntity> adUnitTypeEFCoreRepo)
        {
            AdUnitTypeEFCoreRepo = adUnitTypeEFCoreRepo;
        }

        public async Task<HttpResult<AdUnitTypeDto>> Handle(GetAdUnitTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdUnitTypeDto>> GetOnDb(GetAdUnitTypeByIdQuery request)
        {
            var result = new HttpResult<AdUnitTypeDto>();

            var item = await AdUnitTypeEFCoreRepo.FirstAsync(filter: f => f.Id == request.Id && f.DeletedAt == null);
            if (item == null)
                return result.Fail(404, $"AdUnitType '{request.Id}' doesn't exist.");
                
            var data = Mapper
                .Map(item)
                .ToANew<AdUnitTypeDto>();

            return result.Success(data);
        }
    }
}
