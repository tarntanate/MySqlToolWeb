using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Queries.GetByIdCampaignItemType
{
    public class GetByIdCampaignItemTypeCommandHandler : IRequestHandler<GetByIdCampaignItemTypeCommand, HttpResult<CampaignItemTypeDto>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> CampaignItemTypeMongoDB { get; }

        public GetByIdCampaignItemTypeCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemTypeDocument> campaignItemTypeMongoDB)
        {
            CampaignItemTypeMongoDB = campaignItemTypeMongoDB;
        }

        public async Task<HttpResult<CampaignItemTypeDto>> Handle(GetByIdCampaignItemTypeCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<CampaignItemTypeDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<CampaignItemTypeDto>();
            var item = await CampaignItemTypeMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"ItemType '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<CampaignItemTypeDto>();
            return result.Success(data);
        }
    }
}
