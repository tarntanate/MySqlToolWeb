using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherByName
{
    public class IsExistsPublisherByNameQueryHandler : IRequestHandler<IsExistsPublisherByNameQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<PublisherDocument> PublisherMongoDB { get; }

        public IsExistsPublisherByNameQueryHandler(AdsMongoRepository<PublisherDocument> publisherMongoDB)
        {
            PublisherMongoDB = publisherMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsPublisherByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByNameOnMongo(request.Name);
        }

        private async Task<HttpResult<bool>> IsExistsByNameOnMongo(string name)
        {
            var result = new HttpResult<bool>();
            var isExists = await PublisherMongoDB.AnyAsync(filter: f => f.Name == name);
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Publisher '{name}' doesn't exist.");
        }
    }
}
