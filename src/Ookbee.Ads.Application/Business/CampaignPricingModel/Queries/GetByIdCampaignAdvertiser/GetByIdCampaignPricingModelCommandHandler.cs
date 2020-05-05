using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.GetByIdCampaignPricingModel
{
    public class GetByIdCampaignPricingModelCommandHandler : IRequestHandler<GetByIdCampaignPricingModelCommand, HttpResult<CampaignPricingModelDto>>
    {
        private OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> CampaignPricingModelMongoDB { get; }

        public GetByIdCampaignPricingModelCommandHandler(OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> campaignPricingModelMongoDB)
        {
            CampaignPricingModelMongoDB = campaignPricingModelMongoDB;
        }

        public async Task<HttpResult<CampaignPricingModelDto>> Handle(GetByIdCampaignPricingModelCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<CampaignPricingModelDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<CampaignPricingModelDto>();
            var item = await CampaignPricingModelMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"PricingModel '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<CampaignPricingModelDto>();
            return result.Success(data);
        }
    }
}
