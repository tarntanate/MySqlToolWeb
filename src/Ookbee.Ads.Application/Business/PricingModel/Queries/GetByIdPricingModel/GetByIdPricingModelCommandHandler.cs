﻿using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.MongoDB;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetByIdPricingModel
{
    public class GetByIdPricingModelCommandHandler : IRequestHandler<GetByIdPricingModelCommand, HttpResult<PricingModelDto>>
    {
        private OokbeeAdsMongoDBRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public GetByIdPricingModelCommandHandler(OokbeeAdsMongoDBRepository<PricingModelDocument> pricingModelMongoDB)
        {
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<PricingModelDto>> Handle(GetByIdPricingModelCommand request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Id);
        }

        private async Task<HttpResult<PricingModelDto>> GetOnMongo(string id)
        {
            var result = new HttpResult<PricingModelDto>();
            var item = await PricingModelMongoDB.FirstOrDefaultAsync(filter: f => f.Id == id);
            if (item == null)
                return result.Fail(404, $"PricingModel '{id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<PricingModelDto>();
            return result.Success(data);
        }
    }
}
