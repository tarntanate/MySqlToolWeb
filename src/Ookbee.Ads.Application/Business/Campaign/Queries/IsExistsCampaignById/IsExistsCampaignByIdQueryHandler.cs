﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQueryHandler : IRequestHandler<IsExistsCampaignByIdQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<CampaignDocument> CampaignDocument { get; }

        public IsExistsCampaignByIdQueryHandler(AdsMongoRepository<CampaignDocument> campaignDocument)
        {
            CampaignDocument = campaignDocument;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Id);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string id)
        {
            var result = new HttpResult<bool>();
            var isExists = await CampaignDocument.AnyAsync(filter: f => f.Id == id);
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"Campaign '{id}' doesn't exist.");
        }
    }
}
