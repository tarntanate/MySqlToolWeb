using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.GetByIdCampaignItemAsset;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Commands.UpdateCampaignItemAsset
{
    public class UpdateCampaignItemAssetCommandHandler : IRequestHandler<UpdateCampaignItemAssetCommand, HttpResult<string>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> CampaignItemAssetMongoDB { get; }

        public UpdateCampaignItemAssetCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> campaignItemAssetMongoDB)
        {
            Mediatr = mediatr;
            CampaignItemAssetMongoDB = campaignItemAssetMongoDB;
        }

        public async Task<HttpResult<string>> Handle(UpdateCampaignItemAssetCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignItemAssetDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<string>> UpdateOnMongo(CampaignItemAssetDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var campaignItemResult = await Mediatr.Send(new GetByIdCampaignItemAssetCommand(document.Id));
                if (!campaignItemResult.Ok)
                    return result.Fail(campaignItemResult.StatusCode, campaignItemResult.Message);

                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await CampaignItemAssetMongoDB.UpdateAsync(document.Id, document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
