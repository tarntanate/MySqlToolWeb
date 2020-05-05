using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.IsExistsByNameCampaignAdvertiser;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Commands.CreateCampaignAdvertiser
{
    public class CreateCampaignAdvertiserCommandHandler : IRequestHandler<CreateCampaignAdvertiserCommand, HttpResult<string>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> CampaignAdvertiserMongoDB { get; }

        public CreateCampaignAdvertiserCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> campaignAdvertiserMongoDB)
        {
            Mediatr = mediatr;
            CampaignAdvertiserMongoDB = campaignAdvertiserMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateCampaignAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignAdvertiserDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CampaignAdvertiserDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var isExistsByNameResult = await Mediatr.Send(new IsExistsByNameCampaignAdvertiserCommand(document.Name));
                if (isExistsByNameResult.Data)
                    return result.Fail(409, $"Advertiser already exists.");

                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await CampaignAdvertiserMongoDB.AddAsync(document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }

    }
}
