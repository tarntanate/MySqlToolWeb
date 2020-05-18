using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Driver;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelList
{
    public class GetPricingModelListQueryHandler : IRequestHandler<GetPricingModelListQuery, HttpResult<IEnumerable<PricingModelDto>>>
    {
        private AdsMongoRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public GetPricingModelListQueryHandler(AdsMongoRepository<PricingModelDocument> PricingModelMongoRepo)
        {
            PricingModelMongoDB = PricingModelMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<PricingModelDto>>> Handle(GetPricingModelListQuery request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<PricingModelDto>>> GetListMongoDB(GetPricingModelListQuery request)
        {
            var result = new HttpResult<IEnumerable<PricingModelDto>>();
            var items = await PricingModelMongoDB.FindAsync(
                filter: f => f.EnabledFlag == true,
                sort: Builders<PricingModelDocument>.Sort.Ascending(nameof(PricingModelDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<PricingModelDto>>();
            return result.Success(data);
        }
    }
}
