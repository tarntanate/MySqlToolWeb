using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Queries.IsExistsByIdCampaignItemType
{
    public class IsExistsByIdCampaignItemTypeCommandHandler : IRequestHandler<IsExistsByIdCampaignItemTypeCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> CampaignItemTypeMongoDB { get; }

        public IsExistsByIdCampaignItemTypeCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> campaignItemTypeMongoDB)
        {
            CampaignItemTypeMongoDB = campaignItemTypeMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsByIdCampaignItemTypeCommand request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await CampaignItemTypeMongoDB.AnyAsync(filter: f => f.Id == id);
            return result.Success(isExists);
        }
    }
}
