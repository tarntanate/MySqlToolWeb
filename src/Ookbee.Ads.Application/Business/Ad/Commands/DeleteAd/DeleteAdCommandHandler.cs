﻿using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.DeleteAd
{
    public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsMongoRepository<AdDocument> AdMongoDB { get; }

        public DeleteAdCommandHandler(
            IMediator mediator,
            AdsMongoRepository<AdDocument> adMongoDB)
        {
            Mediator = mediator;
            AdMongoDB = adMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
        {
            var result = await DeleteMongoDB(request);
            return result;
        }

        private async Task<HttpResult<bool>> DeleteMongoDB(DeleteAdCommand request)
        {
            var result = new HttpResult<bool>();

            var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(request.CampaignId));
            if (!isExistsCampaignResult.Ok)
                return isExistsCampaignResult;

            var isExistsResult = await Mediator.Send(new IsExistsAdByIdQuery(request.CampaignId, request.Id));
            if (!isExistsResult.Ok)
                return isExistsResult;

            await AdMongoDB.UpdateManyPartialAsync(
                filter: f => f.Id == request.Id,
                update: Builders<AdDocument>.Update.Set(f => f.EnabledFlag, false)
            );
            return result.Success(true);
        }
    }
}
