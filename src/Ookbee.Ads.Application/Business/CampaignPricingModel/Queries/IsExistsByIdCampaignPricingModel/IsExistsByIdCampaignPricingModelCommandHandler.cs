using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.IsExistsByIdCampaignPricingModel
{
    public class IsExistsByIdCampaignPricingModelCommandHandler : IRequestHandler<IsExistsByIdCampaignPricingModelCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> CampaignPricingModelMongoDB { get; }

        public IsExistsByIdCampaignPricingModelCommandHandler(OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> campaignPricingModelMongoDB)
        {
            CampaignPricingModelMongoDB = campaignPricingModelMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsByIdCampaignPricingModelCommand request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await CampaignPricingModelMongoDB.AnyAsync(filter: f => f.Id == id);
            return result.Success(isExists);
        }
    }
}
