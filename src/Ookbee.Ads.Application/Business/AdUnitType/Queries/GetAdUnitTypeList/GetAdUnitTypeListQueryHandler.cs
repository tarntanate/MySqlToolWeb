using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeList
{
    public class GetAdUnitTypeListQueryHandler : IRequestHandler<GetAdUnitTypeListQuery, HttpResult<IEnumerable<AdUnitTypeDto>>>
    {
        private AdsEFCoreRepository<AdUnitTypeEntity> AdUnitTypeEFCoreRepo { get; }

        public GetAdUnitTypeListQueryHandler(AdsEFCoreRepository<AdUnitTypeEntity> adUnitTypeEFCoreRepo)
        {
            AdUnitTypeEFCoreRepo = adUnitTypeEFCoreRepo;
        }

        public async Task<HttpResult<IEnumerable<AdUnitTypeDto>>> Handle(GetAdUnitTypeListQuery request, CancellationToken cancellationToken)
        {
            return await GetListOnDb(request);
        }

        private async Task<HttpResult<IEnumerable<AdUnitTypeDto>>> GetListOnDb(GetAdUnitTypeListQuery request)
        {
            var result = new HttpResult<IEnumerable<AdUnitTypeDto>>();

            var items = await AdUnitTypeEFCoreRepo.FindAsync(
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );
            
            var data = Mapper
                .Map(items)
                .ToANew<IEnumerable<AdUnitTypeDto>>();

            return result.Success(data);
        }
    }
}
