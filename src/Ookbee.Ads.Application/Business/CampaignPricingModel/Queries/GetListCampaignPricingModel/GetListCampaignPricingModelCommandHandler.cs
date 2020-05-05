using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.CampaignPricingModel.Queries.GetListCampaignPricingModel
{
    public class GetListCampaignPricingModelCommandHandler : IRequestHandler<GetListCampaignPricingModelCommand, HttpResult<IEnumerable<CampaignPricingModelDto>>>
    {
        private OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> CampaignPricingModelMongoDB { get; }

        public GetListCampaignPricingModelCommandHandler(OokbeeAdsMongoDBRepository<CampaignPricingModelDocument> campaignPricingModelMongoRepo)
        {
            CampaignPricingModelMongoDB = campaignPricingModelMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<CampaignPricingModelDto>>> Handle(GetListCampaignPricingModelCommand request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<CampaignPricingModelDto>>> GetListMongoDB(GetListCampaignPricingModelCommand request)
        {
            var result = new HttpResult<IEnumerable<CampaignPricingModelDto>>();
            var items = await CampaignPricingModelMongoDB.FindAsync(
                sort: Builders<CampaignPricingModelDocument>.Sort.Descending(nameof(CampaignPricingModelDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<CampaignPricingModelDto>>();
            return result.Success(data);
        }
    }
}
