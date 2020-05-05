using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.IsExistsByNameCampaignAdvertiser
{
    public class IsExistsByNameCampaignAdvertiserCommandHandler : IRequestHandler<IsExistsByNameCampaignAdvertiserCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> CampaignAdvertiserMongoDB { get; }

        public IsExistsByNameCampaignAdvertiserCommandHandler(OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> campaignAdvertiserMongoDB)
        {
            CampaignAdvertiserMongoDB = campaignAdvertiserMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsByNameCampaignAdvertiserCommand request, CancellationToken cancellationToken)
        {
            return await IsExistsByNameOnMongo(request.Name);
        }

        private async Task<HttpResult<bool>> IsExistsByNameOnMongo(string name)
        {
            var result = new HttpResult<bool>();
            var isExists = await CampaignAdvertiserMongoDB.AnyAsync(filter: f => f.Name == name);
            return result.Success(isExists);
        }
    }
}
