using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherByName
{
    public class GetPublisherByNameQueryHandler : IRequestHandler<GetPublisherByNameQuery, Response<PublisherDto>>
    {
        private readonly AdsDbRepository<PublisherEntity> PublisherDbRepo;

        public GetPublisherByNameQueryHandler(
            AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<PublisherDto>> Handle(GetPublisherByNameQuery request, CancellationToken cancellationToken)
        {
            var item = await PublisherDbRepo.FirstAsync<PublisherDto>(
                filter: f =>
                    f.Name.ToUpper() == request.Name.ToUpper() &&
                    f.CountryCode.ToUpper() == request.CountryCode.ToUpper() &&
                    f.DeletedAt == null
            );

            var result = new Response<PublisherDto>();
            return (item != null)
                ? result.OK(item)
                : result.NotFound();
        }
    }
}
