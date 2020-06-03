using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherById
{
    public class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQuery, HttpResult<PublisherDto>>
    {
        private AdsMongoRepository<PublisherDocument> PublisherMongoDB { get; }

        public GetPublisherByIdQueryHandler(AdsMongoRepository<PublisherDocument> publisherMongoDB)
        {
            PublisherMongoDB = publisherMongoDB;
        }

        public async Task<HttpResult<PublisherDto>> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<PublisherDto>> GetOnMongo(GetPublisherByIdQuery request)
        {
            var result = new HttpResult<PublisherDto>();
            var item = await PublisherMongoDB.FirstOrDefaultAsync(
                filter: f => f.Id == request.Id && 
                             f.DeletedAt == null
            );
            if (item == null)
                return result.Fail(404, $"Publisher '{request.Id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<PublisherDto>();
            return result.Success(data);
        }
    }
}
