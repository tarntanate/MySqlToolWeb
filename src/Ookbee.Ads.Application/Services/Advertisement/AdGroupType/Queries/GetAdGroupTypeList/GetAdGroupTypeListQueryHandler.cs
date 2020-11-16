using MediatR;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.GetAdGroupTypeList
{
    public class GetAdGroupTypeListQueryHandler : IRequestHandler<GetAdGroupTypeListQuery, Response<IEnumerable<AdGroupTypeDto>>>
    {
        private readonly AdsDbRepository<AdGroupTypeEntity> AdGroupTypeDbRepo;

        public GetAdGroupTypeListQueryHandler(
            AdsDbRepository<AdGroupTypeEntity> adGroupTypeDbRepo)
        {
            AdGroupTypeDbRepo = adGroupTypeDbRepo;
        }

        public async Task<Response<IEnumerable<AdGroupTypeDto>>> Handle(GetAdGroupTypeListQuery request, CancellationToken cancellationToken)
        {
            var items = await AdGroupTypeDbRepo.FindAsync<AdGroupTypeDto>(
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<AdGroupTypeDto>>();
            return (items.HasValue())
                ? result.OK(items)
                : result.NotFound();
        }
    }
}
