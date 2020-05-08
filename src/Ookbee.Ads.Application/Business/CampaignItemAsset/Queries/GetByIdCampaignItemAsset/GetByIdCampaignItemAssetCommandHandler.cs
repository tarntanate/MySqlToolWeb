using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.GetByIdCampaignItemAsset
{
    public class GetByIdCampaignItemAssetCommandHandler : IRequestHandler<GetByIdCampaignItemAssetCommand, HttpResult<CampaignItemAssetDto>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> CampaignItemAssetMongoDB { get; }

        public GetByIdCampaignItemAssetCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> campaignItemAssetMongoDB)
        {
            CampaignItemAssetMongoDB = campaignItemAssetMongoDB;
        }

        public async Task<HttpResult<CampaignItemAssetDto>> Handle(GetByIdCampaignItemAssetCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<CampaignItemAssetDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<CampaignItemAssetDto>();
            var item = await CampaignItemAssetMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"Advertiser '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<CampaignItemAssetDto>();
            return result.Success(data);
        }
    }
}
