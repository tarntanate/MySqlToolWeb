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
            return await IsExistsByNameOnMongo(request);
        }

        private async Task<HttpResult<bool>> IsExistsByNameOnMongo(IsExistsPublisherByNameQuery request)
        {
            var result = new HttpResult<bool>();
            var isExists = await PublisherMongoDB.AnyAsync(
                filter: f => f.Name == request.Name && 
                             f.DeletedAt == null
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Publisher '{request.Name}' doesn't exist.");
        }
    }
}
