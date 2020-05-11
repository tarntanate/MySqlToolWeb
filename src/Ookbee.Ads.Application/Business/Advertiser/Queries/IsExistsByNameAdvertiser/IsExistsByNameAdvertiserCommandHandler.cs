using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsByNameAdvertiser
{
    public class IsExistsByNameAdvertiserCommandHandler : IRequestHandler<IsExistsByNameAdvertiserCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<AdvertiserDocument> AdvertiserMongoDB { get; }

        public IsExistsByNameAdvertiserCommandHandler(OokbeeAdsMongoDBRepository<AdvertiserDocument> advertiserMongoDB)
        {
            AdvertiserMongoDB = advertiserMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsByNameAdvertiserCommand request, CancellationToken cancellationToken)
        {
            return await IsExistsByNameOnMongo(request.Name);
        }

        private async Task<HttpResult<bool>> IsExistsByNameOnMongo(string name)
        {
            var result = new HttpResult<bool>();
            var isExists = await AdvertiserMongoDB.AnyAsync(filter: f => f.Name == name);
            return result.Success(isExists);
        }
    }
}
