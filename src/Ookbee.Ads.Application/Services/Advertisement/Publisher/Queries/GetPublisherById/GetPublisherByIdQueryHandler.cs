using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherById
{
    public class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQuery, Response<PublisherDto>>
    {
        private readonly AdsDbRepository<PublisherEntity> PublisherDbRepo;

        public GetPublisherByIdQueryHandler(
            AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<PublisherDto>> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
        {
            var item = await PublisherDbRepo.FirstAsync(
                selector: PublisherDto.Projection,
                filter: f =>
                    f.Id == request.Id &&
                    f.DeletedAt == null
            );

            var result = new Response<PublisherDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
