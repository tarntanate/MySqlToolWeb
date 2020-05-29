﻿using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdByName;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdByName;
using Ookbee.Ads.Application.Business.AdSlot.Queries.GetAdSlotById;
using Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAd
{
    public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, HttpResult<string>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public CreateAdCommandHandler(
            IMediator mediator,
            AdsMongoRepository<AdDocument> adMongoDB)
        {
            Mediator = mediator;
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<string>> Handle(CreateAdCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateMongoDB(request);
            return result;
        }

        private async Task<HttpResult<string>> CreateMongoDB(CreateAdCommand request)
        {
            var result = new HttpResult<string>();
            try
            {
                var campaignResult = await Mediator.Send(new GetCampaignByIdQuery(request.CampaignId));
                if (!campaignResult.Ok)
                    return result.Fail(campaignResult.StatusCode, campaignResult.Message);

                var adSlotTypeResult = await Mediator.Send(new GetAdSlotByIdQuery(request.AdSlotId));
                if (!adSlotTypeResult.Ok)
                    return result.Fail(adSlotTypeResult.StatusCode, adSlotTypeResult.Message);

                var adResult = await Mediator.Send(new GetAdByNameQuery(request.CampaignId, request.Name));
                if (adResult.Ok &&
                    adResult.Data.Id != request.Id &&
                    adResult.Data.Name == request.Name)
                    return result.Fail(409, $"Ad '{request.Name}' already exists.");

                var now = MechineDateTime.Now;
                var document = Mapper.Map(request).ToANew<AdDocument>();
                document.CreatedDate = now.DateTime;
                document.UpdatedDate = now.DateTime;
                await AdMongoDB.AddAsync(document);
                return result.Success(document.Id);
            }
            catch (Exception ex)
            {
                return result.Fail(500, ex.Message);
            }
        }
    }
}
