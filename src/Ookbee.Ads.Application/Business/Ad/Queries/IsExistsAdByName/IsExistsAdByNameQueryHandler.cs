using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdByName
{
    public class IsExistsAdByNameQueryHandler : IRequestHandler<IsExistsAdByNameQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public IsExistsAdByNameQueryHandler(AdsMongoRepository<AdDocument> adMongoDB)
        {
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsAdByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.CampaignId, request.AdSlotId, request.Name);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string campaignId, string adSlotId, string name)
        {
            var result = new HttpResult<bool>();
            var isExists = await AdMongoDB.AnyAsync(
                filter: f => f.Campaign.Id == campaignId &&
                             f.AdSlot.Id == adSlotId &&
                             f.Name == name &&
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Ad '{name}' doesn't exist.");
        }
    }
}
