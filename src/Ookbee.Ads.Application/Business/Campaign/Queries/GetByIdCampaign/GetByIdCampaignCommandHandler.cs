using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetByIdCampaign
{
    public class GetByIdCampaignCommandHandler : IRequestHandler<GetByIdCampaignCommand, HttpResult<CampaignDto>>
    {
        private OokbeeAdsMongoDBRepository<CampaignDocument> CampaignMongoDB { get; }

        public GetByIdCampaignCommandHandler(OokbeeAdsMongoDBRepository<CampaignDocument> campaignMongoDB)
        {
            CampaignMongoDB = campaignMongoDB;
        }

        public async Task<HttpResult<CampaignDto>> Handle(GetByIdCampaignCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<CampaignDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<CampaignDto>();
            var item = await CampaignMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"This Campaign doesn't exist.");
            var data = Mapper.Map(item).ToANew<CampaignDto>();
            return result.Success(data);
        }
    }
}
