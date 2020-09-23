using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.GetPublisherByName
{
    public class GetPublisherByNameQueryHandler : IRequestHandler<GetPublisherByNameQuery, Response<PublisherDto>>
    {
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public GetPublisherByNameQueryHandler(AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<PublisherDto>> Handle(GetPublisherByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await PublisherDbRepo.FirstAsync(
                selector: PublisherDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null
            );

            var result = new Response<PublisherDto>();
            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Publisher '{request.Name}' doesn't exist.");
        }
    }
}
