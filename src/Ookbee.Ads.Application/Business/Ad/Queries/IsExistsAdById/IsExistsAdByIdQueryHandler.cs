using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById
{
    public class IsExistsAdByIdQueryHandler : IRequestHandler<IsExistsAdByIdQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public IsExistsAdByIdQueryHandler(AdsMongoRepository<AdDocument> adMongoDB)
        {
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await AdMongoDB.AnyAsync(
                filter: f => f.Id == id && 
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Ad '{id}' doesn't exist.");
        }
    }
}
