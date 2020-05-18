using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherByName
{
    public class GetPublisherByNameQueryHandler : IRequestHandler<GetPublisherByNameQuery, HttpResult<PublisherDto>>
    {
        private AdsMongoRepository<PublisherDocument> PublisherMongoDB { get; }

        public GetPublisherByNameQueryHandler(AdsMongoRepository<PublisherDocument> publisherMongoDB)
        {
            PublisherMongoDB = publisherMongoDB;
        }

        public async Task<HttpResult<PublisherDto>> Handle(GetPublisherByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Name);
        }

        private async Task<HttpResult<PublisherDto>> GetOnMongo(string name)
        {
            var result = new HttpResult<PublisherDto>();
            var item = await PublisherMongoDB.FirstOrDefaultAsync(
                filter: f => f.Name == name && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"Publisher '{name}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<PublisherDto>();
            return result.Success(data);
        }
    }
}
