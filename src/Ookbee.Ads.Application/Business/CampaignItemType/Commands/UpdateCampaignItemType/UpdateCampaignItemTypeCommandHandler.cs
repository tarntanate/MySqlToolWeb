using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Commands.UpdateCampaignItemType
{
    public class UpdateCampaignItemTypeCommandHandler : IRequestHandler<UpdateCampaignItemTypeCommand, HttpResult<bool>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> CampaignItemTypeMongoDB { get; }

        public UpdateCampaignItemTypeCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> campaignItemTypeMongoDB)
        {
            CampaignItemTypeMongoDB = campaignItemTypeMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignItemTypeCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignItemTypeDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(CampaignItemTypeDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await CampaignItemTypeMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
