using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherById
{
    public class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQuery, HttpResult<PublisherDto>>
    {
        private AdsDbRepository<PublisherEntity> PublisherDbRepo { get; }

        public GetPublisherByIdQueryHandler(AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<HttpResult<PublisherDto>> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<PublisherDto>> GetOnDb(GetPublisherByIdQuery request)
        {
            var result = new HttpResult<PublisherDto>();

            var item = await PublisherDbRepo.FirstAsync(filter: f => 
                f.Id == request.Id && 
                f.DeletedAt == null
            );
            
            if (item == null)
                return result.Fail(404, $"Publisher '{request.Id}' doesn't exist.");
                
            var data = Mapper
                .Map(item)
                .ToANew<PublisherDto>();

            return result.Success(data);
        }
    }
}
