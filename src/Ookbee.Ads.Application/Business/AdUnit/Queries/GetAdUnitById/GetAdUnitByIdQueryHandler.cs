using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitById
{
    public class GetAdUnitByIdQueryHandler : IRequestHandler<GetAdUnitByIdQuery, HttpResult<AdUnitDto>>
    {
        private AdsEFCoreRepository<AdUnitEntity> AdUnitEFCoreRepo { get; }

        public GetAdUnitByIdQueryHandler(AdsEFCoreRepository<AdUnitEntity> adUnitEFCoreRepo)
        {
            AdUnitEFCoreRepo = adUnitEFCoreRepo;
        }

        public async Task<HttpResult<AdUnitDto>> Handle(GetAdUnitByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<AdUnitDto>> GetOnDb(GetAdUnitByIdQuery request)
        {
            var result = new HttpResult<AdUnitDto>();

            var item = await AdUnitEFCoreRepo.FirstAsync(filter: f => f.Id == request.Id && f.DeletedAt == null);
            if (item == null)
                return result.Fail(404, $"AdUnit '{request.Id}' doesn't exist.");
                
            var data = Mapper
                .Map(item)
                .ToANew<AdUnitDto>();

            return result.Success(data);
        }
    }
}
