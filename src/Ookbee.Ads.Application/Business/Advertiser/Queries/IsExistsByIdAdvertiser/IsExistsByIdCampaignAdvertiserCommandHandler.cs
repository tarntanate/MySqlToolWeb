using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsByIdAdvertiser
{
    public class IsExistsByIdAdvertiserCommandHandler : IRequestHandler<IsExistsByIdAdvertiserCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public IsExistsByIdAdvertiserCommandHandler(OokbeeAdsMongoDBRepository<AdvertiserDocument> advertiserMongoDB)
        {
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsByIdAdvertiserCommand request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await AdvertiserMongoDB.AnyAsync(filter: f => f.Id == id);
            return result.Success(isExists);
        }
    }
}
