using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.IsExistsByIdCampaignItemAsset
{
    public class IsExistsByIdCampaignItemAssetCommandHandler : IRequestHandler<IsExistsByIdCampaignItemAssetCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> CampaignItemAssetMongoDB { get; }

        public IsExistsByIdCampaignItemAssetCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> campaignItemAssetMongoDB)
        {
            CampaignItemAssetMongoDB = campaignItemAssetMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsByIdCampaignItemAssetCommand request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await CampaignItemAssetMongoDB.AnyAsync(filter: f => f.Id == id);
            return result.Success(isExists);
        }
    }
}
