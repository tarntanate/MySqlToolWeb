using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Documents;
using Ookbee.Ads.Persistence.Advertising.Mongo;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelByName
{
    public class IsExistsPricingModelByNameQueryHandler : IRequestHandler<IsExistsPricingModelByNameQuery, HttpResult<bool>>
    {
        private AdsMongoRepository<PricingModelDocument> PricingModelMongoDB { get; }

        public IsExistsPricingModelByNameQueryHandler(AdsMongoRepository<PricingModelDocument> pricingModelMongoDB)
        {
            PricingModelMongoDB = pricingModelMongoDB;
        }

        public async Task<HttpResult<bool>> Handle(IsExistsPricingModelByNameQuery request, CancellationToken cancellationToken)
        {
            return await IsExistsByIdOnMongo(request.Name);
        }

        private async Task<HttpResult<bool>> IsExistsByIdOnMongo(string name)
        {
            var result = new HttpResult<bool>();
            var isExists = await PricingModelMongoDB.AnyAsync(
                filter: f => f.Name == name && 
                             f.EnabledFlag == true
            );
            if (isExists)
                return result.Success(true);
            return result.Fail(404, $"PricingModel '{name}' doesn't exist.");
        }
    }
}
