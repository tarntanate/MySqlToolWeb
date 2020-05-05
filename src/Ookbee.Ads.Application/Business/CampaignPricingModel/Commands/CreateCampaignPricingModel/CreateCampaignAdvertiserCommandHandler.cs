using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Commands.CreateCampaignPricingModel
{
    public class CreateCampaignPricingModelCommandHandler : IRequestHandler<CreateCampaignPricingModelCommand, HttpResult<string>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> CampaignPricingModelMongoDB { get; }

        public CreateCampaignPricingModelCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> campaignPricingModelMongoDB)
        {
            Mediatr = mediatr;
            CampaignPricingModelMongoDB = campaignPricingModelMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateCampaignPricingModelCommand request, CancellationToken cancellationToken)
        {
            var document = Mapper.Map(request).ToANew<CampaignPricingModelDocument>();
            var result = await CreateMongoDB(document);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CampaignPricingModelDocument document)
        {
            var result = new HttpResult<string>();
            try
            {
                var now = MechineDateTime.Now;
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await CampaignPricingModelMongoDB.AddAsync(document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
