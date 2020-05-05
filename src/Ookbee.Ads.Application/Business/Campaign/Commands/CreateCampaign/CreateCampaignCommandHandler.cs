﻿using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.GetByIdCampaignAdvertiser;
using Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.GetByIdCampaignPricingModel;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, HttpResult<string>>
    {
        private IMediator Mediatr { get; }
        private OokbeeAdsMongoDBRepository<CampaignDocument> CampaignMongoDB { get; }

        public CreateCampaignCommandHandler(
            IMediator mediatr,
            OokbeeAdsMongoDBRepository<CampaignDocument> campaignMongoDB)
        {
            CampaignMongoDB = campaignMongoDB;
            Mediatr = mediatr;
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
                var advertiserResult = await Mediatr.Send(new GetByIdCampaignAdvertiserCommand(request.AdvertiserId));
                if (!advertiserResult.Ok)
                    return result.Fail(400, advertiserResult.Message);

                var pricingModelResult = await Mediatr.Send(new GetByIdCampaignPricingModelCommand(request.PricingModelId));
                if (!pricingModelResult.Ok)
                    return result.Fail(400, pricingModelResult.Message);

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<CampaignDocument>();
                document.Advertiser = Mapper.Map(advertiserResult.Data).ToANew<CampaignAdvertiserDocument>();
                document.PricingModel = Mapper.Map(pricingModelResult.Data).ToANew<CampaignPricingModelDocument>();
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await CampaignMongoDB.AddAsync(document);
                return result.Success(document.Id.ToString());
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
