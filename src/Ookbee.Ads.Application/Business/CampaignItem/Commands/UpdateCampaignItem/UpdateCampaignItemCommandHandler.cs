using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItem.Commands.UpdateCampaignItem
{
    public class UpdateCampaignItemCommandHandler : IRequestHandler<UpdateCampaignItemCommand, HttpResult<bool>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignItemDocument> CampaignItemMongoDB { get; }

        public UpdateCampaignItemCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignItemDocument> campaignItemMongoDB)
        {
            Mediatr = mediatr;
            CampaignItemMongoDB = campaignItemMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignItemCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignItemDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(CampaignItemDocument document)
        {
            var result = new HttpResult<bool>();
            try
            {
                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await CampaignItemMongoDB.UpdateAsync(document.Id, document);
                return result.Success(true);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
