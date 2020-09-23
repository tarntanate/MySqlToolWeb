using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Queries.GetAdGroupList
{
    public class GetAdGroupListQueryHandler : IRequestHandler<GetAdGroupListQuery, Response<IEnumerable<AdGroupDto>>>
    {
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public GetAdGroupListQueryHandler(AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Response<IEnumerable<AdGroupDto>>> Handle(GetAdGroupListQuery request, CancellationToken cancellationToken)
        {
            var items = await AdGroupDbRepo.FindAsync(
                selector: AdGroupDto.Projection,
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<AdGroupDto>>();
            return result.Success(items);
        }
    }
}
