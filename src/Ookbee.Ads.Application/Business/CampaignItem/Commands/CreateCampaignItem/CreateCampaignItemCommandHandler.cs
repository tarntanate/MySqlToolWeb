using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetByIdCampaign;
using Ookbee.Ads.Application.Business.CampaignItemType.Queries.GetByIdCampaignItemType;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItem.Commands.CreateCampaignItem
{
    public class CreateCampaignItemCommandHandler : IRequestHandler<CreateCampaignItemCommand, HttpResult<string>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignItemDocument> CampaignItemMongoDB { get; }

        public CreateCampaignItemCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignItemDocument> campaignItemMongoDB)
        {
            Mediatr = mediatr;
            CampaignItemMongoDB = campaignItemMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateCampaignItemCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignItemDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CampaignItemDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var campaignResult = await Mediatr.Send(new GetByIdCampaignCommand(document.CampaignId));
                if (!campaignResult.Ok)
                    return result.Fail(campaignResult.StatusCode, campaignResult.Message);

                var campaignItemTypeResult = await Mediatr.Send(new GetByIdCampaignItemTypeCommand(document.CampaignId));
                if (!campaignItemTypeResult.Ok)
                    return result.Fail(campaignItemTypeResult.StatusCode, campaignItemTypeResult.Message);

                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await CampaignItemMongoDB.AddAsync(document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }

    }
}
