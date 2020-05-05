using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.IsExistsByIdCampaignAdvertiser
{
    public class IsExistsByIdCampaignAdvertiserCommandHandler : IRequestHandler<IsExistsByIdCampaignAdvertiserCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> CampaignAdvertiserMongoDB { get; }

        public IsExistsByIdCampaignAdvertiserCommandHandler(OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> campaignAdvertiserMongoDB)
        {
            CampaignAdvertiserMongoDB = campaignAdvertiserMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsByIdCampaignAdvertiserCommand request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await CampaignAdvertiserMongoDB.AnyAsync(filter: f => f.Id == id);
            return result.Success(isExists);
        }
    }
}
