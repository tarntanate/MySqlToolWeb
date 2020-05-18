﻿using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignById
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, HttpResult<CampaignDto>>
    {
        private AdsMongoRepository<CampaignDocument> CampaignMongoDB { get; }

        public GetCampaignByIdQueryHandler(AdsMongoRepository<CampaignDocument> campaignMongoDB)
        {
            CampaignMongoDB = campaignMongoDB;
        }

        public async Task<HttpResult<CampaignDto>> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<CampaignDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<CampaignDto>();
            var item = await CampaignMongoDB.FirstOrDefaultAsync(
                filter: f => f.Id == id && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"This Campaign doesn't exist.");
            var data = Mapper.Map(item).ToANew<CampaignDto>();
            return result.Success(data);
        }
    }
}