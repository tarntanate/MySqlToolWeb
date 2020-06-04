using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListCommandHandler : IRequestHandler<GetCampaignListCommand, HttpResult<IEnumerable<CampaignDto>>>
    {
        private AdsMongoRepository<CampaignDocument> CampaignMongoDB { get; }

        public GetCampaignListCommandHandler(AdsMongoRepository<CampaignDocument> CampaignMongoRepo)
        {
            CampaignMongoDB = CampaignMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<CampaignDto>>> Handle(GetCampaignListCommand request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<CampaignDto>>> GetListMongoDB(GetCampaignListCommand request)
        {
            var result = new HttpResult<IEnumerable<CampaignDto>>();
            var items = await CampaignMongoDB.FindAsync(
                filter: f => f.DeletedAt == null,
                sort: Builders<CampaignDocument>.Sort.Ascending(nameof(CampaignDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<CampaignDto>>();
            return result.Success(data);
        }
    }
}
