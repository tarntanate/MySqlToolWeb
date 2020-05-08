using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItem.Queries.GetByIdCampaignItem
{
    public class GetByIdCampaignItemCommandHandler : IRequestHandler<GetByIdCampaignItemCommand, HttpResult<CampaignItemDto>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemDocument> CampaignItemMongoDB { get; }

        public GetByIdCampaignItemCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemDocument> campaignItemMongoDB)
        {
            CampaignItemMongoDB = campaignItemMongoDB;
        }

        public async Task<HttpResult<CampaignItemDto>> Handle(GetByIdCampaignItemCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<CampaignItemDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<CampaignItemDto>();
            var item = await CampaignItemMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"Advertiser '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<CampaignItemDto>();
            return result.Success(data);
        }
    }
}
