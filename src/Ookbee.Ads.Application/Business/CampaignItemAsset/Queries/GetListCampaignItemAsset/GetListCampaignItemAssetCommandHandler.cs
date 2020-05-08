using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.GetListCampaignItemAsset
{
    public class GetListCampaignItemAssetCommandHandler : IRequestHandler<GetListCampaignItemAssetCommand, HttpResult<IEnumerable<CampaignItemAssetDto>>>
    {
        private OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> CampaignItemAssetMongoDB { get; }

        public GetListCampaignItemAssetCommandHandler(OokbeeAdsMongoDBRepository<CampaignItemAssetDocument> campaignItemAssetMongoDB)
        {
            CampaignItemAssetMongoDB = campaignItemAssetMongoDB;
        }

        public async Task<HttpResult<IEnumerable<CampaignItemAssetDto>>> Handle(GetListCampaignItemAssetCommand request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<CampaignItemAssetDto>>> GetListMongoDB(GetListCampaignItemAssetCommand request)
        {
            var result = new HttpResult<IEnumerable<CampaignItemAssetDto>>();
            var items = await CampaignItemAssetMongoDB.FindAsync(
                sort: Builders<CampaignItemAssetDocument>.Sort.Descending(nameof(CampaignItemAssetDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<CampaignItemAssetDto>>();
            return result.Success(data);
        }
    }
}
