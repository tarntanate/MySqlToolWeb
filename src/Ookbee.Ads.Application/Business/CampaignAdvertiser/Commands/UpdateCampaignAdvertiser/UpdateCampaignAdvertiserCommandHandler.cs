using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.IsExistsByIdCampaignAdvertiser;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Commands.UpdateCampaignAdvertiser
{
    public class UpdateCampaignAdvertiserCommandHandler : IRequestHandler<UpdateCampaignAdvertiserCommand, HttpResult<string>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> CampaignAdvertiserMongoDB { get; }

        public UpdateCampaignAdvertiserCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> campaignAdvertiserMongoDB)
        {
            Mediatr = mediatr;
            CampaignAdvertiserMongoDB = campaignAdvertiserMongoDB;
        }

        public async Task<HttpResult<string>> Handle(UpdateCampaignAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignAdvertiserDocument>();
            var result = await UpdateOnMongo(document);
            return result;
        }

        private async Task<HttpResult<string>> UpdateOnMongo(CampaignAdvertiserDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var isExistsByNameResult = await Mediatr.Send(new IsExistsByIdCampaignAdvertiserCommand(document.Id));
                if (!isExistsByNameResult.Data)
                    return result.Fail(404, $"Advertiser doesn't exist.");

                var now = MechineDateTime.Now;
                document.UpdatedDate = now.DateTime;
                await CampaignAdvertiserMongoDB.UpdateAsync(document.Id, document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
