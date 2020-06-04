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
            return await IsExistsByIdOnMongo(request);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(IsExistsPublisherByIdQuery request)
        {
            var result = new HttpResult<bool>();
            var isExists = await PublisherMongoDB.AnyAsync(
                filter: f => f.Id == request.Id && 
                             f.DeletedAt == null
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Publisher '{request.Id}' doesn't exist.");
        }
    }
}
