using AgileObjects.AgileMapper;
using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelByName
{
    public class GetPricingModelByNameQueryHandler : IRequestHandler<GetPricingModelByNameQuery, HttpResult<PricingModelDto>>
    {
        private AdsMongoRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public GetPricingModelByNameQueryHandler(AdsMongoRepository<PricingModelDocument> pricingModelMongoDB)
        {
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<PricingModelDto>> Handle(GetPricingModelByNameQuery request, CancellationToken cancellationToken)
        {
            return await GetOnMongo(request.Name);
        }

        private async Task<HttpResult<PricingModelDto>> GetOnMongo(string name)
        {
            var result = new HttpResult<PricingModelDto>();
            var item = await PricingModelMongoDB.FirstOrDefaultAsync(
                filter: f => f.Name == name && 
                             f.EnabledFlag == true
            );
            if (item == null)
                return result.Fail(404, $"PricingModel '{name}' doesn't exist.");
            var data = Mapper.Map(item).ToANew<PricingModelDto>();
            return result.Success(data);
        }
    }
}
