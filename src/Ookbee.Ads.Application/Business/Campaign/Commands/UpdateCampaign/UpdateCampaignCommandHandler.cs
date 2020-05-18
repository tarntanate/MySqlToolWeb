using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignByName;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelById;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<CampaignDocument> CampaignMongoDB { get; }

        public UpdateCampaignCommandHandler(
            IMediator mediator,
            AdsMongoRepository<CampaignDocument> campaignMongoDB)
        {
            Mediator = mediator;
            CampaignMongoDB = campaignMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnMongo(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnMongo(UpdateCampaignCommand request)
        {
            var result = new HttpResult<bool>();
            try
            {
                var isExistsResult = await Mediator.Send(new IsExistsCampaignByIdQuery(request.Id));
                if (!isExistsResult.Ok)
                    return isExistsResult;
                    
                var advertiserResult = await Mediator.Send(new GetAdvertiserByIdQuery(request.AdvertiserId));
                if (!advertiserResult.Ok)
                    return result.Fail(400, advertiserResult.Message);

                var pricingModelResult = await Mediator.Send(new GetPricingModelByIdQuery(request.PricingModelId));
                if (!pricingModelResult.Ok)
                    return result.Fail(400, pricingModelResult.Message);

                var adSlotResult = await Mediator.Send(new GetCampaignByNameQuery(request.Name));
                if (adSlotResult.Ok &&
                    adSlotResult.Data.Id != request.Id &&
                    adSlotResult.Data.Advertiser.Id == request.AdvertiserId &&
                    adSlotResult.Data.PricingModel.Id == request.PricingModelId &&
                    adSlotResult.Data.Name == request.Name)
                    return result.Fail(409, $"Campaign '{request.Name}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<CampaignDocument>();
                document.Advertiser = Mapper.Map(advertiserResult.Data).ToANew<DefaultDocument>();
                document.PricingModel = Mapper.Map(pricingModelResult.Data).ToANew<DefaultDocument>();
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
