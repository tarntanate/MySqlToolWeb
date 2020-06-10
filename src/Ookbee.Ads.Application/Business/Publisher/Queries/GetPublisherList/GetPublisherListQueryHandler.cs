using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherList
{
    public class GetPublisherListQueryHandler : IRequestHandler<GetPublisherListQuery, HttpResult<IEnumerable<PublisherDto>>>
    {
        private AdsEFCoreRepository<PublisherEntity> PublisherEFCoreRepo { get; }

        public GetPublisherListQueryHandler(AdsEFCoreRepository<PublisherEntity> publisherEFCoreRepo)
        {
            PublisherEFCoreRepo = publisherEFCoreRepo;
        }

        public async Task<HttpResult<IEnumerable<PublisherDto>>> Handle(GetPublisherListQuery request, CancellationToken cancellationToken)
        {
            return await GetListOnDb(request);
        }

        private async Task<HttpResult<IEnumerable<PublisherDto>>> GetListOnDb(GetPublisherListQuery request)
        {
            var result = new HttpResult<IEnumerable<PublisherDto>>();

            var items = await PublisherEFCoreRepo.FindAsync(
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var data = Mapper
                .Map(items)
                .ToANew<IEnumerable<PublisherDto>>();

            return result.Success(data);
        }
    }
}
