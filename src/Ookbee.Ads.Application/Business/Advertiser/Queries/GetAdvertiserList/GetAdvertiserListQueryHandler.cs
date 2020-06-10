using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserList
{
    public class GetAdvertiserListQueryHandler : IRequestHandler<GetAdvertiserListQuery, HttpResult<IEnumerable<AdvertiserDto>>>
    {
        private AdsEFCoreRepository<AdvertiserEntity> AdvertiserEFCoreRepo { get; }

        public GetAdvertiserListQueryHandler(AdsEFCoreRepository<AdvertiserEntity> advertiserEFCoreRepo)
        {
            AdvertiserEFCoreRepo = advertiserEFCoreRepo;
        }

        public async Task<HttpResult<IEnumerable<AdvertiserDto>>> Handle(GetAdvertiserListQuery request, CancellationToken cancellationToken)
        {
            return await GetListOnDb(request);
        }

        private async Task<HttpResult<IEnumerable<AdvertiserDto>>> GetListOnDb(GetAdvertiserListQuery request)
        {
            var result = new HttpResult<IEnumerable<AdvertiserDto>>();

            var items = await AdvertiserEFCoreRepo.FindAsync(
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var data = Mapper
                .Map(items)
                .ToANew<IEnumerable<AdvertiserDto>>();

            return result.Success(data);
        }
    }
}
