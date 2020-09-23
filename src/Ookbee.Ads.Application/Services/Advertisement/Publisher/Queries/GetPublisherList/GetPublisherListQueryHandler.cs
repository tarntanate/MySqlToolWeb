using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherList
{
    public class GetPublisherListQueryHandler : IRequestHandler<GetPublisherListQuery, Response<IEnumerable<PublisherDto>>>
    {
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public GetPublisherListQueryHandler(AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<IEnumerable<PublisherDto>>> Handle(GetPublisherListQuery request, CancellationToken cancellationToken)
        {
            var items = await PublisherDbRepo.FindAsync(
                selector: PublisherDto.Projection,
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            var result = new Response<IEnumerable<PublisherDto>>();
            return result.Success(items);
        }
    }
}
