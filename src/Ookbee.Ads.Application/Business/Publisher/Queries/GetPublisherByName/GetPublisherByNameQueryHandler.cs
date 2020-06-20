using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherByName
{
    public class GetPublisherByNameQueryHandler : IRequestHandler<GetPublisherByNameQuery, HttpResult<PublisherDto>>
    {
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public GetPublisherByNameQueryHandler(AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<HttpResult<PublisherDto>> Handle(GetPublisherByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<PublisherDto>> GetOnDb(GetPublisherByNameQuery request)
        {
            var result = new HttpResult<PublisherDto>();

            var item = await PublisherDbRepo.FirstAsync(
                selector: PublisherDto.Projection,
                filter: f =>
                    f.Name == request.Name &&
                    f.DeletedAt == null
            );

            return (item != null)
                ? result.Success(item)
                : result.Fail(404, $"Publisher '{request.Name}' doesn't exist.");
        }
    }
}
