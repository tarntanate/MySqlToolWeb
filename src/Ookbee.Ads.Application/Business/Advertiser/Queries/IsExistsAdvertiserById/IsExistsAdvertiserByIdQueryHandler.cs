using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById
{
    public class IsExistsAdvertiserByIdQueryHandler : IRequestHandler<IsExistsAdvertiserByIdQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public IsExistsAdvertiserByIdQueryHandler(AdsMongoRepository<AdvertiserDocument> advertiserMongoDB)
        {
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdvertiserByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(IsExistsAdvertiserByIdQuery request)
        {
            var result = new HttpResult<bool>();
            var isExists = await AdvertiserMongoDB.AnyAsync(
                filter: f => f.Id == request.Id && 
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Advertiser '{request.Id}' doesn't exist.");
        }
    }
}
