using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.GetListCampaignAdvertiser
{
    public class GetListCampaignAdvertiserCommandHandler : IRequestHandler<GetListCampaignAdvertiserCommand, HttpResult<IEnumerable<CampaignAdvertiserDto>>>
    {
        private OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> CampaignAdvertiserMongoDB { get; }

        public GetListCampaignAdvertiserCommandHandler(OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> campaignAdvertiserMongoDB)
        {
            CampaignAdvertiserMongoDB = campaignAdvertiserMongoDB;
        }

        public async Task<HttpResult<IEnumerable<CampaignAdvertiserDto>>> Handle(GetListCampaignAdvertiserCommand request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<CampaignAdvertiserDto>>> GetListMongoDB(GetListCampaignAdvertiserCommand request)
        {
            var result = new HttpResult<IEnumerable<CampaignAdvertiserDto>>();
            var items = await CampaignAdvertiserMongoDB.FindAsync(
                sort: Builders<CampaignAdvertiserDocument>.Sort.Descending(nameof(CampaignAdvertiserDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<CampaignAdvertiserDto>>();
            return result.Success(data);
        }
    }
}
