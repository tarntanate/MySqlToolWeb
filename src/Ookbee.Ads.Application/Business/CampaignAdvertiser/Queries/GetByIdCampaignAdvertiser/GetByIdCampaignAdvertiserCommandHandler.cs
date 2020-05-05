using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.GetByIdCampaignAdvertiser
{
    public class GetByIdCampaignAdvertiserCommandHandler : IRequestHandler<GetByIdCampaignAdvertiserCommand, HttpResult<CampaignAdvertiserDto>>
    {
        private OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> CampaignAdvertiserMongoDB { get; }

        public GetByIdCampaignAdvertiserCommandHandler(OokbeeAdsMongoDBRepository<CampaignAdvertiserDocument> campaignAdvertiserMongoDB)
        {
            CampaignAdvertiserMongoDB = campaignAdvertiserMongoDB;
        }

        public async Task<HttpResult<CampaignAdvertiserDto>> Handle(GetByIdCampaignAdvertiserCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<CampaignAdvertiserDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<CampaignAdvertiserDto>();
            var item = await CampaignAdvertiserMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"Advertiser '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<CampaignAdvertiserDto>();
            return result.Success(data);
        }
    }
}
