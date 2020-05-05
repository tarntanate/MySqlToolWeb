using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<CampaignDocument> CampaignMongoDB { get; }

        public UpdateCampaignCommandHandler(OokbeeAdsMongoDBRepository<CampaignDocument> campaignMongoDB)
        {
            CampaignMongoDB = campaignMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(CampaignDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await CampaignMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
