using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherById
{
    public class IsExistsPublisherByIdQueryHandler : IRequestHandler<IsExistsPublisherByIdQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<PublisherDocument> PublisherMongoDB { get; }

        public IsExistsPublisherByIdQueryHandler(AdsMongoRepository<PublisherDocument> publisherMongoDB)
        {
            PublisherMongoDB = publisherMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsPublisherByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await PublisherMongoDB.AnyAsync(
                filter: f => f.Id == id && 
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Publisher '{id}' doesn't exist.");
        }
    }
}
