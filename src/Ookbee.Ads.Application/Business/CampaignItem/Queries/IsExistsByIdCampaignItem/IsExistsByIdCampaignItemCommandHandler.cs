using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItem.Queries.IsExistsByIdCampaignItem
{
    public class IsExistsByIdCampaignItemCommandHandler : IRequestHandler<IsExistsByIdCampaignItemCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemDocument> CampaignItemMongoDB { get; }

        public IsExistsByIdCampaignItemCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemDocument> campaignItemMongoDB)
        {
            CampaignItemMongoDB = campaignItemMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsByIdCampaignItemCommand request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await CampaignItemMongoDB.AnyAsync(filter: f => f.Id == id);
            return result.Success(isExists);
        }
    }
}
