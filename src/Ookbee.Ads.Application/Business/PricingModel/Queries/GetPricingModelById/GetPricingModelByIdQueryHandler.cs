﻿using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelById
{
    public class GetPricingModelByIdQueryHandler : IRequestHandler<GetPricingModelByIdQuery, HttpResult<PricingModelDto>>
    {
        private AdsMongoRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public GetPricingModelByIdQueryHandler(AdsMongoRepository<PricingModelDocument> pricingModelMongoDB)
        {
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<PricingModelDto>> Handle(GetPricingModelByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request);
        }

        private async Task<HttpResult<PricingModelDto>> GetOnMongo(GetPricingModelByIdQuery request)
        {
            var result = new HttpResult<PricingModelDto>();
            var item = await PricingModelMongoDB.FirstOrDefaultAsync(
                filter: f => f.Id == request.Id && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"PricingModel '{request.Id}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<PricingModelDto>();
            return result.Success(data);
        }
    }
}
