using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.GetAdvertiserList
{
    public class GetAdvertiserListQueryHandler : IRequestHandler<GetAdvertiserListQuery, HttpResult<IEnumerable<AdvertiserDto>>>
    {
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public GetAdvertiserListQueryHandler(AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdvertiserDto>>> Handle(GetAdvertiserListQuery request, CancellationToken cancellationToken)
        {
            var items = await AdvertiserDbRepo.FindAsync(
                selector: AdvertiserDto.Projection,
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var result = new HttpResult<IEnumerable<AdvertiserDto>>();
            return result.Success(items);
        }
    }
}
