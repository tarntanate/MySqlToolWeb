using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItem.Queries.GetListCampaignItem
{
    public class GetListCampaignItemCommandHandler : IRequestHandler<GetListCampaignItemCommand, HttpResult<IEnumerable<CampaignItemDto>>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemDocument> CampaignItemMongoDB { get; }

        public GetListCampaignItemCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemDocument> campaignItemMongoDB)
        {
            CampaignItemMongoDB = campaignItemMongoDB;
        }

        public async Task<HttpResult<IEnumerable<CampaignItemDto>>> Handle(GetListCampaignItemCommand request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<CampaignItemDto>>> GetListMongoDB(GetListCampaignItemCommand request)
        {
            var result = new HttpResult<IEnumerable<CampaignItemDto>>();
            var items = await CampaignItemMongoDB.FindAsync(
                sort: Builders<CampaignItemDocument>.Sort.Descending(nameof(CampaignItemDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<CampaignItemDto>>();
            return result.Success(data);
        }
    }
}
