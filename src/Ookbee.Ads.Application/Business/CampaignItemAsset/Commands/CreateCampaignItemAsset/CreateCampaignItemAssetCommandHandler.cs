using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.CampaignItem.Queries.GetByIdCampaignItem;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Commands.CreateCampaignItemAsset
{
    public class CreateCampaignItemAssetCommandHandler : IRequestHandler<CreateCampaignItemAssetCommand, HttpResult<string>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> CampaignItemAssetMongoDB { get; }

        public CreateCampaignItemAssetCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> campaignItemAssetMongoDB)
        {
            Mediatr = mediatr;
            CampaignItemAssetMongoDB = campaignItemAssetMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateCampaignItemAssetCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignItemAssetDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CampaignItemAssetDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var campaignResult = await Mediatr.Send(new GetByIdCampaignItemCommand(document.CampaignItemId));
                if (!campaignResult.Ok)
                    return result.Fail(campaignResult.StatusCode, campaignResult.Message);

                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await CampaignItemAssetMongoDB.AddAsync(document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }

    }
}
