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

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetListPricingModel
{
    public class GetListPricingModelCommandHandler : IRequestHandler<GetListPricingModelCommand, HttpResult<IEnumerable<PricingModelDto>>>
    {
        private OokbeeAdsMongoDBRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public GetListPricingModelCommandHandler(OokbeeAdsMongoDBRepository<PricingModelDocument> PricingModelMongoRepo)
        {
            PricingModelMongoDB = PricingModelMongoRepo;
        }

        public async Task<HttpResult<IEnumerable<PricingModelDto>>> Handle(GetListPricingModelCommand request, CancellationToken cancellationToken)
        {
            return await GetListMongoDB(request);
        }

        private async Task<HttpResult<IEnumerable<PricingModelDto>>> GetListMongoDB(GetListPricingModelCommand request)
        {
            var result = new HttpResult<IEnumerable<PricingModelDto>>();
            var items = await PricingModelMongoDB.FindAsync(
                sort: Builders<PricingModelDocument>.Sort.Descending(nameof(PricingModelDocument.Name)),
                start: request.Start,
                length: request.Length
            );
            var data = Mapper.Map(items).ToANew<IEnumerable<PricingModelDto>>();
            return result.Success(data);
        }
    }
}
