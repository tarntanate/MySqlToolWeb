using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Queries.GetAdUnitTypeList
{
    public class GetAdUnitTypeListQueryHandler : IRequestHandler<GetAdUnitTypeListQuery, HttpResult<IEnumerable<AdUnitTypeDto>>>
    {
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public GetAdUnitTypeListQueryHandler(AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdUnitTypeDto>>> Handle(GetAdUnitTypeListQuery request, CancellationToken cancellationToken)
        {
            var items = await AdUnitTypeDbRepo.FindAsync(
                selector: AdUnitTypeDto.Projection,
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var result = new HttpResult<IEnumerable<AdUnitTypeDto>>();
            return result.Success(items);
        }
    }
}
