using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserById;
using Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelById;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<CampaignDocument> CampaignMongoDB { get; }

        public CreateCampaignCommandHandler(
            IMediator mediator,
            AdsMongoRepository<CampaignDocument> campaignMongoDB)
        {
            CampaignMongoDB = campaignMongoDB;
            Mediator = mediator;
        }

        public async Task<HttpResult<string>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreateCampaignCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var advertiserResult = await Mediator.Send(new GetAdvertiserByIdQuery(request.AdvertiserId));
                if (!advertiserResult.Ok)
                    return result.Fail(400, advertiserResult.Message);

                var pricingModelResult = await Mediator.Send(new GetPricingModelByIdQuery(request.PricingModelId));
                if (!pricingModelResult.Ok)
                    return result.Fail(400, pricingModelResult.Message);

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<CampaignDocument>();
                document.Advertiser = Mapper.Map(advertiserResult.Data).ToANew<AdvertiserDocument>();
                document.PricingModel = Mapper.Map(pricingModelResult.Data).ToANew<PricingModelDocument>();
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await CampaignMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
