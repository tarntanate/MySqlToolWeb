using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignByName
{
    public class GetCampaignByNameQueryHandler : IRequestHandler<GetCampaignByNameQuery, HttpResult<CampaignDto>>
    {
        private AdsMongoRepository<CampaignDocument> CampaignMongoDB { get; }

        public GetCampaignByNameQueryHandler(AdsMongoRepository<CampaignDocument> campaignMongoDB)
        {
            CampaignMongoDB = campaignMongoDB;
        }

        public async Task<HttpResult<CampaignDto>> Handle(GetCampaignByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<CampaignDto>> GetOnMongo(GetCampaignByNameQuery request)
        {
            var result = new HttpResult<CampaignDto>();
            var item = await CampaignMongoDB.FirstOrDefaultAsync(
                filter: f => f.Name == request.Name && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"Campaign '{request.Name}'doesn't exist.");
            var data = Mapper.Map(item).ToANew<CampaignDto>();
            return result.Success(data);
        }
    }
}
